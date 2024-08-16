using Scellecs.Morpeh;
using TriInspector;
using UnityEngine;
using UnityEngine.AI;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct AgentPathComponent : IComponent {
    public float destinationProximity;
    public float pathUpdateInterval;

    public float lastUpdatePath;
    public Vector3 destination;
    public Vector3[] path;
    public int pathNodeIdx;
}