using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PursuerToExplorerSystem))]
public sealed class PursuerToExplorerSystem : UpdateSystem {
    AspectFactory<MobileAgentAspect> _agentFactory;
    Filter _pursuers;

    public override void OnAwake() {
        _agentFactory = World.GetAspectFactory<MobileAgentAspect>();
        _pursuers = World.Filter.
            Extend<MobileAgentAspect>().
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