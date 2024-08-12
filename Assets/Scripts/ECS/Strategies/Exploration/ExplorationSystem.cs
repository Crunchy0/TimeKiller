using Scellecs.Morpeh.Providers;
using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using System.Collections.Generic;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.AI;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ExplorationSystem))]
public sealed class ExplorationSystem : UpdateSystem {
    AspectFactory<MobileAgentAspect> _agentFactory;
    Filter _explorerFilter;
    GroupRelationMatrix _relMatrix;

    public override void OnAwake() {
        _agentFactory = World.GetAspectFactory<MobileAgentAspect>();
        _explorerFilter = World.Filter.Extend<MobileAgentAspect>().With<ExplorerComponent>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach (Entity e in _explorerFilter)
        {
            var mobileAgent = _agentFactory.Get(e);

            var body = mobileAgent.Body;
            ref var agent = ref mobileAgent.AiAgent;
            ref var actor = ref mobileAgent.Actor;
            ref var explorer = ref e.GetComponent<ExplorerComponent>();

            if (DetectEnemy(mobileAgent) || actor.currentZone == null)
                continue;

            if (agent.pathNodeIdx < 0 || agent.pathNodeIdx >= agent.path.Length)
                actor.target = actor.eye.position + actor.eye.forward;
            else
                actor.target = agent.path[agent.pathNodeIdx];

            bool requiresTarget = explorer.targetZone == null;
            bool reachedTarget = (agent.destination - body.transform.position).magnitude < 1f;

            if (!requiresTarget && !reachedTarget)
                continue;

            float exploreRand = Random.Range(0f, 1f);
            if (exploreRand > actor.config.ExplorationPenchant)
                explorer.targetZone = actor.currentZone;
            else
                explorer.targetZone = actor.currentZone.ChooseNextZone(actor.config.GroupId);
            agent.destination = explorer.targetZone.GetRandomPoint();
        }
    }

    private bool DetectEnemy(MobileAgentAspect mobileAgent)
    {
        bool detected = false;
        Entity e = mobileAgent.Entity;
        var actor = mobileAgent.Actor;

        foreach (Collider col in Physics.OverlapSphere(actor.eye.position, actor.config.SightRange, LayerMask.GetMask("Population")))
        {
            if (!IsInSight(col.bounds, actor))
                continue;
            var provider = col.transform.GetComponentInParent<EntityProvider>();
            if (provider == null || !provider.Entity.Has<ActorComponent>())
                continue;

            var otherActorComp = provider.Entity.GetComponent<ActorComponent>();
            var relation = _relMatrix.GetRelations(actor.config.GroupId)[(int)(otherActorComp.config.GroupId)];
            //Debug.Log($"{e} detected neighbour {provider.Entity}, relation: {relation}");

            if (relation != GroupRelation.ENEMY)
                continue;

            //ref var agent = ref mobileAgent.AiAgent;
            Timer timer = new Timer(actor.config.LostTargetPursuitDuration);
            TimerManager.Subscribe(timer.Update);
            var pursue = new PursueTargetComponent
            {
                targetEntityId = provider.Entity.ID,
                targetTransform = provider.transform,
                isTargetInSight = true,
                stopPursuitTimer = timer
            };

            //agent.destination = pursue.targetTransform.position;
            e.RemoveComponent<ExplorerComponent>();
            e.SetComponent(pursue);
            detected = true;
            break;
        }

        return detected;
    }

    private bool IsInSight(Bounds bounds, ActorComponent actor)
    {
        var eye = actor.eye.position;
        Vector3 dir = (bounds.center - eye);
        if (dir.magnitude > actor.config.SightRange)
            return false;

        float angleHor = Mathf.Abs(Vector3.SignedAngle(actor.eye.forward, dir, actor.eye.up));
        float angleVer = Mathf.Abs(Vector3.SignedAngle(actor.eye.forward, dir, actor.eye.right));
        if (angleHor > actor.config.Fov.x / 2 || angleVer > actor.config.Fov.y / 2)
            return false;

        // TODO: Adjust to the collider's transform (up/down vectors)
        Vector3 bottom = bounds.center + (bounds.size.y / 2) * Vector3.down;
        Vector3 top = bottom + bounds.size.y * Vector3.up;
        bool isBottomVisible = true;
        bool isTopVisible = true;
        if (Physics.Raycast(eye, bottom - eye, out var hitBottom, actor.config.SightRange))
        {
            bool hitAgent = hitBottom.transform.gameObject.layer == LayerMask.NameToLayer("Population");
            if (!hitAgent)
                isBottomVisible = false;
        }
        if (Physics.Raycast(eye, top - eye, out var hitTop, actor.config.SightRange))
        {
            bool hitAgent = hitTop.transform.gameObject.layer == LayerMask.NameToLayer("Population");
            if (!hitAgent)
                isTopVisible = false;
        }

        if (isBottomVisible || isTopVisible)
            Debug.DrawLine(actor.eye.position, actor.eye.position + dir, Color.green);

        return isBottomVisible || isTopVisible;
    }

    [Inject]
    private void InjectDependencies(GroupRelationMatrix relations)
    {
        _relMatrix = relations;
    }
}