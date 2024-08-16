using Scellecs.Morpeh.Providers;
using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.AI;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class AgentPathProvider : MonoProvider<AgentPathComponent> {
    protected override void Initialize()
    {
        ref var agentPath = ref GetData();
        agentPath.lastUpdatePath = -agentPath.pathUpdateInterval;
        agentPath.destination = transform.position;
        agentPath.path = new Vector3[0];
        agentPath.pathNodeIdx = -1;
    }
}