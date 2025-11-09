using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct AnimationBasedAttackerComponent : IComponent
{
    [Min(0)] public float cooldown;
    [Min(1)] public int numberOfAttacks;
    [HideInInspector] public float startTime;
    [HideInInspector] public float stopTime;
}