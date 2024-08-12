using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PursueTargetSystem))]
public sealed class PursueTargetSystem : UpdateSystem {
    AspectFactory<MobileAgentAspect> _agentFactory;
    Filter _pursuers;

    public override void OnAwake()
    {
        _agentFactory = World.GetAspectFactory<MobileAgentAspect>();
        _pursuers = World.Filter.Extend<MobileAgentAspect>().With<PursueTargetComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach(Entity e in _pursuers)
        {
            var mobileAgent = _agentFactory.Get(e);
            ref var actor = ref mobileAgent.Actor;
            ref var pursue = ref e.GetComponent<PursueTargetComponent>();

            if(!World.TryGetEntity(pursue.targetEntityId, out var targetEntity)
                || targetEntity.IsNullOrDisposed()
                || !targetEntity.Has<MovementComponent>())
                continue;

            Collider col = pursue.targetTransform.GetComponentInChildren<Collider>();
            if (col == null)
                pursue.isTargetInSight = false;
            else if (IsInSight(col.bounds, actor, pursue.targetEntityId))
                pursue.isTargetInSight = true;
            else if (pursue.isTargetInSight)
            {
                Debug.Log("Lost target");
                pursue.isTargetInSight = false;
                pursue.stopPursuitTimer.Reset();
            }

            bool dropTarget = !pursue.isTargetInSight && pursue.stopPursuitTimer.IsExpired;
            if (dropTarget)
            {
                Debug.Log("Dropped target");
                TimerManager.Unsubscribe(pursue.stopPursuitTimer.Update);
                e.RemoveComponent<PursueTargetComponent>();
                e.AddComponent<ExplorerComponent>();
                continue;
            }

            var body = mobileAgent.Body;
            ref var agent = ref mobileAgent.AiAgent;
            float distance = (body.transform.position - pursue.targetTransform.position).magnitude;

            if (pursue.isTargetInSight)
                actor.target = pursue.targetTransform.position;     // Look at the prey
            else if (agent.pathNodeIdx >= 0 && agent.pathNodeIdx < agent.path.Length)
                actor.target = agent.path[agent.pathNodeIdx];

            if (pursue.isTargetInSight && distance <= actor.config.AttackRange)
            {
                // Enter "Attack" state, still pursuing
                if (!e.Has<AttackTargetComponent>())
                {
                    ref var attack = ref e.AddComponent<AttackTargetComponent>();
                    attack.targetEntityId = pursue.targetEntityId;
                    attack.targetTransform = pursue.targetTransform;
                    attack.isConfigured = false;
                }
                // Not coming too close
                agent.destination = body.transform.position;
                if (body.rigidbody.velocity.magnitude > 0.1f)
                    body.rigidbody.velocity *= 0.97f;
            }
            else
            {
                // Just keep following the prey
                var targetMovement = targetEntity.GetComponent<MovementComponent>();
                agent.destination = pursue.targetTransform.position;
                agent.destination.x += targetMovement.direction.x;
                agent.destination.z += targetMovement.direction.y;
            }
        }
    }

    private bool IsInSight(Bounds bounds, ActorComponent actor, EntityId targetId)
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
        if(Physics.Raycast(eye, bottom - eye, out var hitBottom, actor.config.SightRange))
        {
            bool hitAgent = hitBottom.transform.gameObject.layer == LayerMask.NameToLayer("Population");
            if (!hitAgent)
                isBottomVisible = false;
            else
            {
                var provider = hitBottom.transform.GetComponentInParent<EntityProvider>();
                if (provider == null || provider.Entity.ID != targetId)
                    isBottomVisible = false;
            }
        }
        if(Physics.Raycast(eye, top - eye, out var hitTop, actor.config.SightRange))
        {
            bool hitAgent = hitTop.transform.gameObject.layer == LayerMask.NameToLayer("Population");
            if (!hitAgent)
                isTopVisible = false;
            else
            {
                var provider = hitTop.transform.GetComponentInParent<EntityProvider>();
                if (provider == null || provider.Entity.ID != targetId)
                    isTopVisible = false;
            }
        }

        if(isBottomVisible || isTopVisible)
            Debug.DrawLine(actor.eye.position, actor.eye.position + dir, Color.green);

        return isBottomVisible || isTopVisible;
    }
}