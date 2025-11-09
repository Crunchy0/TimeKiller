using Scellecs.Morpeh.Providers;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class TimeRangedAttackerProvider : MonoProvider<TimeRangedAttackerComponent>
{
    protected override void Initialize()
    {
        ref var attacker = ref GetData();
        var curTime = Time.time;
        attacker.startTime = curTime - attacker.span - attacker.cooldown;
        attacker.stopTime = curTime - attacker.cooldown;
    }
}