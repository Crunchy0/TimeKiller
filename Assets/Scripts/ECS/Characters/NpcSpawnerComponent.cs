using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct NpcSpawnerComponent : IComponent
{
    public Zone zone;
    public CharacterGroupId ownerId;
    [Min(0)] public float lastSpawnTime;
    [Min(1)] public float spawnInterval;
}