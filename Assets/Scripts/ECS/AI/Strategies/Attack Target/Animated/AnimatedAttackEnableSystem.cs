using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class AnimatedAttackEnableSystem : CustomUpdateSystem
{
    Filter _attackers;

    public override void OnAwake()
    {
        _attackers = World.Filter.
            With<AttackTargetComponent>().
            With<AnimationBasedAttackerComponent>().
            With<AnimatedCharacterComponent>().
            Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach(Entity e in _attackers)
        {
            ref var attack = ref e.GetComponent<AnimationBasedAttackerComponent>();
            var animated = e.GetComponent<AnimatedCharacterComponent>();

            float curTime = Time.time;
            float start = attack.startTime;
            float stop = attack.stopTime;
            float cooldown = attack.cooldown;

            if(start < stop && stop + cooldown < curTime)
            {
                int attackId = Random.Range(0, attack.numberOfAttacks - 1);
                animated.controller.TriggerAttack(attackId);
                attack.startTime = curTime;
            }
        }
    }
}