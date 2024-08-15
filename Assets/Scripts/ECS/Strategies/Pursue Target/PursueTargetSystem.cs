using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class PursueTargetSystem : CustomUpdateSystem
{
    AspectFactory<MobileAgentAspect> _agentFactory;
    Filter _pursuers;
    Color _connectionColor = new Color(0.95f, 0.05f, 0.7f);

    public override void OnAwake()
    {
        _agentFactory = World.GetAspectFactory<MobileAgentAspect>();
        _pursuers = World.Filter.
            Extend<MobileAgentAspect>().
            With<TargetObserverComponent>().
            With<TargetPursuerComponent>().
            Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach(Entity e in _pursuers)
        {
            var mobileAgent = _agentFactory.Get(e);
            ref var actor = ref mobileAgent.Actor;
            ref var target = ref e.GetComponent<TargetObserverComponent>();

            if (!World.TryGetEntity(target.id, out var targetEntity) || targetEntity.IsNullOrDisposed())
                continue;

            var body = mobileAgent.Body;
            ref var agent = ref mobileAgent.AiAgent;
            Debug.DrawLine(body.transform.position, target.transform.position, _connectionColor);

            // This should be in a separate system!
            if (target.isInSight)
                actor.lookTarget = target.transform.position;     // Look at the prey
            else if (agent.pathNodeIdx >= 0 && agent.pathNodeIdx < agent.path.Length)
                actor.lookTarget = agent.path[agent.pathNodeIdx];
            //else
            //    actor.lookTarget = WHERE???


            float distance = (mobileAgent.Body.transform.position - target.transform.position).magnitude;
            if (target.isInSight && distance <= actor.config.AttackRange)
            {
                // Not coming too close
                agent.destination = body.transform.position;
                if (body.rigidbody.velocity.magnitude > 0.1f)
                    body.rigidbody.velocity *= 0.97f;
            }
            else
            {
                // Just keep following the prey
                agent.destination = target.transform.position;
                if (targetEntity.Has<MovementComponent>())
                {
                    var targetMovement = targetEntity.GetComponent<MovementComponent>();
                    agent.destination.x += targetMovement.direction.x;
                    agent.destination.z += targetMovement.direction.y;
                }
            }
        }
    }
}