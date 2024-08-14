using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(FromAttackerSystem))]
public sealed class FromAttackerSystem : UpdateSystem {
    AspectFactory<MobileAgentAspect> _agentFactory;
    Filter _attackers;
    Event<PrimaryActionEvent> _primEvt;

    public override void OnAwake()
    {
        _agentFactory = World.GetAspectFactory<MobileAgentAspect>();
        _attackers = World.Filter.
            Extend<MobileAgentAspect>().
            With<TargetObserverComponent>().
            With<AttackTargetComponent>().
            Build();
        _primEvt = World.GetEvent<PrimaryActionEvent>();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (Entity e in _attackers)
        {
            var mobileAgent = _agentFactory.Get(e);
            var target = e.GetComponent<TargetObserverComponent>();

            if (target.IsAcquired)
            {
                float distance = (target.transform.position - mobileAgent.Body.transform.position).magnitude;
                if (distance <= mobileAgent.Actor.config.AttackRange)
                    continue;
            }

            var attack = e.GetComponent<AttackTargetComponent>();
            if (attack.startTime > attack.stopTime)
                _primEvt.NextFrame(new PrimaryActionEvent { actorId = e.ID, activated = false });
            e.RemoveComponent<AttackTargetComponent>();
        }
    }
}