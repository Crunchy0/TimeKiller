using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(AttackTargetSystem))]
public sealed class AttackTargetSystem : UpdateSystem {
    AspectFactory<MobileAgentAspect> _agentFactory;
    Filter _attackersFilter;
    Event<PrimaryActionEvent> _primEvt;

    public override void OnAwake() {
        _attackersFilter = World.Filter.Extend<MobileAgentAspect>().With<AttackTargetComponent>().With<ActiveEquipment>().Build();
        _agentFactory = World.GetAspectFactory<MobileAgentAspect>();
        _primEvt = World.GetEvent<PrimaryActionEvent>();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(Entity e in _attackersFilter)
        {
            var mobileAgent = _agentFactory.Get(e);
            var body = mobileAgent.Body;
            var actor = mobileAgent.Actor;
            ref var attack = ref e.GetComponent<AttackTargetComponent>();
            float distance = (attack.targetTransform.position - body.transform.position).magnitude;

            if (!attack.isConfigured)
                ConfigureAttack(e);

            if(distance > actor.config.AttackRange && attack.attackSpan.IsExpired)
            {
                // Detach attack component, remove all timers
                TimerManager.Unsubscribe(attack.attackSpan.Update);
                TimerManager.Unsubscribe(attack.attackCooldown.Update);
                e.RemoveComponent<AttackTargetComponent>();
                continue;
            }
        }

    }

    public void ConfigureAttack(Entity e)
    {
        var mobileAgent = _agentFactory.Get(e);
        var actor = mobileAgent.Actor;
        ref var attack = ref e.GetComponent<AttackTargetComponent>();

        attack.attackSpan = new Timer(actor.config.AttackSpan);
        attack.attackCooldown = new Timer(actor.config.AttackCooldown);

        attack.attackSpan.TimerExpired += attack.attackCooldown.Reset;
        attack.attackSpan.TimerExpired += () =>
        {
            _primEvt.NextFrame(new PrimaryActionEvent { actorId = e.ID, activated = false });
        };

        attack.attackCooldown.TimerExpired += attack.attackSpan.Reset;
        attack.attackCooldown.TimerExpired += () =>
        {
            _primEvt.NextFrame(new PrimaryActionEvent { actorId = e.ID, activated = true });
        };

        attack.attackSpan.Reset();

        TimerManager.Subscribe(attack.attackSpan.Update);
        TimerManager.Subscribe(attack.attackCooldown.Update);

        attack.isConfigured = true;

        _primEvt.NextFrame(new PrimaryActionEvent { actorId = e.ID, activated = true });
    }
}