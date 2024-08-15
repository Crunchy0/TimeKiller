using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.AI;
using Unity.IL2CPP.CompilerServices;
using TriInspector;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct ActorComponent : IComponent {
    [HideInEditMode] public Zone currentZone;
    [HideInInspector] public Vector3 lookTarget;
    [Required] public Transform equipmentLocation;
    public Transform eye;
    public CharacterConfig config;
}