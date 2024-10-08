using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class PursuerToExplorerSystem : CustomUpdateSystem
{
    AspectFactory<AgentAspect> _agentFactory;
    Filter _pursuers;

    public override void OnAwake() {
        _agentFactory = World.GetAspectFactory<AgentAspect>();
        _pursuers = World.Filter.
            Extend<AgentAspect>().
            With<TargetObserverComponent>().
            With<TargetPursuerComponent>().
            Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(Entity e in _pursuers)
        {
            ref var target = ref e.GetComponent<TargetObserverComponent>();
            if (!target.IsAcquired)
            {
                // We lost the target
                e.AddComponent<ExplorerComponent>();
                e.RemoveComponent<TargetPursuerComponent>();
            }
        }
        
    }
}