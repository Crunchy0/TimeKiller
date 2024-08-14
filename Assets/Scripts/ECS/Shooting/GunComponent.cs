using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct GunComponent : IComponent {
    public GunConfig config;
    public int ammoLeft;
    public bool isAutoEnabled;
    public bool isTriggerPulled;
    public bool shotOnce;
    public float lastShotTime;
}