using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class AnimationBasedAttackerProvider : MonoProvider<AnimationBasedAttackerComponent>
{
    protected override void Initialize()
    {
        ref var attacker = ref GetData();
        attacker.stopTime = Time.time - attacker.cooldown;
        attacker.startTime = attacker.stopTime - 1;
    }

    public void StopAttack()
    {
        var req = World.Default.GetRequest<AnimatedAttackStopRequest>();
        req.Publish(new AnimatedAttackStopRequest { actorId = Entity.ID }, true);
    }
}