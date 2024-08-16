using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class AttackTargetSystem : CustomUpdateSystem
{
    AspectFactory<MobileAgentAspect> _agentFactory;
    Filter _attackers;
    Event<PrimaryActionEvent> _primEvt;

    public override void OnAwake() {
        _agentFactory = World.GetAspectFactory<MobileAgentAspect>();
        _attackers = World.Filter.
            Extend<MobileAgentAspect>().
            With<TargetObserverComponent>().
            With<AttackTargetComponent>().
            With<ActiveEquipment>().
            Build();
        
        _primEvt = World.GetEvent<PrimaryActionEvent>();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(Entity e in _attackers)
        {
            ref var attack = ref e.GetComponent<AttackTargetComponent>();

            float curTime = Time.time;
            float start = attack.startTime;
            float stop = attack.stopTime;
            float span = attack.span;
            float cooldown = attack.cooldown;

            if (stop > start && stop + cooldown < curTime)
            {
                attack.startTime = curTime;
                _primEvt.NextFrame(new PrimaryActionEvent { actorId = e.ID, activated = true });
            }
            else if (start > stop && start + span < curTime)
            {
                attack.stopTime = curTime;
                _primEvt.NextFrame(new PrimaryActionEvent { actorId = e.ID, activated = false });
            }
        }

    }
}