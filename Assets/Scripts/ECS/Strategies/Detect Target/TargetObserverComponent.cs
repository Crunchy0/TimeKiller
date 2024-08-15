using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct TargetObserverComponent : IComponent {
    public bool IsAcquired { get => lastTimeSeen != -forgetTime; }

    //[Header("Configuration")]
    public float sightDistance;
    public Vector2 fov;
    public float forgetTime;

    public EntityId id;
    public Transform transform;
    public float lastTimeSeen;
    public bool isInSight;

    public void Forget()
    {
        id = EntityId.Invalid;
        transform = null;
        lastTimeSeen = -forgetTime;
        isInSight = false;
    }
}