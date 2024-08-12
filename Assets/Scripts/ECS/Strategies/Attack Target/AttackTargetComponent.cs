using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct AttackTargetComponent : IComponent {
    public EntityId targetEntityId;
    public Transform targetTransform;
    public Timer attackSpan;
    public Timer attackCooldown;
    public bool isConfigured;
}