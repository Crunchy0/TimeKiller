using Scellecs.Morpeh;
using TriInspector;
using UnityEngine;
using UnityEngine.AI;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct AiAgentComponent : IComponent {
    public Timer pathUpdateTimer;
    public Vector3 destination;
    public Vector3[] path;
    public int pathNodeIdx;
}