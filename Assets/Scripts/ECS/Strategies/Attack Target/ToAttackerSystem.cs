using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ToAttackerSystem))]
public sealed class ToAttackerSystem : UpdateSystem {
    AspectFactory<MobileAgentAspect> _agentFactory;
    Filter _potentialAttackers;

    public override void OnAwake()
    {
        _agentFactory = World.GetAspectFactory<MobileAgentAspect>();
        _potentialAttackers = World.Filter.
            Extend<MobileAgentAspect>().
            With<TargetObserverComponent>().
            Without<AttackTargetComponent>().
            Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (Entity e in _potentialAttackers)
        {
            var mobileAgent = _agentFactory.Get(e);
            var actor = mobileAgent.Actor;
            var target = e.GetComponent<TargetObserverComponent>();

            if (!target.isInSight)
                continue;

            float distance = (mobileAgent.Body.transform.position - target.transform.position).magnitude;
            if (distance <= actor.config.AttackRange)
            {
                ref var attack = ref e.AddComponent<AttackTargetComponent>();
                float curTime = Time.time;

                attack.span = actor.config.AttackSpan;
                attack.cooldown = actor.config.AttackCooldown;
                attack.startTime = curTime - attack.span - attack.cooldown;
                attack.stopTime = curTime - attack.cooldown;
            }
        }
    }
}