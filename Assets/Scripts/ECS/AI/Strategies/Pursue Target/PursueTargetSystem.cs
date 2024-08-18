using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class PursueTargetSystem : CustomUpdateSystem
{
    AspectFactory<AgentAspect> _agentFactory;
    Filter _pursuers;
    Color _connectionColor = new Color(0.95f, 0.05f, 0.7f);

    public override void OnAwake()
    {
        _agentFactory = World.GetAspectFactory<AgentAspect>();
        _pursuers = World.Filter.
            Extend<AgentAspect>().
            With<AgentPathComponent>().
            With<TargetObserverComponent>().
            With<TargetPursuerComponent>().
            Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach(Entity e in _pursuers)
        {
            var agent = _agentFactory.Get(e);
            ref var actor = ref agent.Actor;
            ref var target = ref e.GetComponent<TargetObserverComponent>();

            if (!target.IsAcquired)
                continue;

            var body = agent.Body;
            ref var agentPath = ref e.GetComponent<AgentPathComponent>();
            //Debug.DrawLine(body.transform.position, target.transform.position, _connectionColor);

            float distance = (agent.Body.transform.position - target.transform.position).magnitude;
            if (target.isInSight && distance <= actor.config.AttackRange)
            {
                // Not coming too close
                agentPath.destination = body.transform.position;
                if (body.rigidbody.velocity.magnitude > 0.1f)
                    body.rigidbody.velocity *= 0.97f;
            }
            else
            {
                // Just keep following the prey
                if (!World.TryGetEntity(target.id, out var targetEntity) || targetEntity.IsNullOrDisposed())
                    continue;

                agentPath.destination = target.transform.position;
                if (targetEntity.Has<MovementComponent>())
                {
                    var targetMovement = targetEntity.GetComponent<MovementComponent>();
                    agentPath.destination.x += targetMovement.direction.x;
                    agentPath.destination.z += targetMovement.direction.y;
                }
            }
        }
    }
}