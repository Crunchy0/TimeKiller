using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ExplorerToPursuerSystem))]
public sealed class ExplorerToPursuerSystem : UpdateSystem {
    AspectFactory<MobileAgentAspect> _agentFactory;
    Filter _explorersWithTarget;

    public override void OnAwake()
    {
        _agentFactory = World.GetAspectFactory<MobileAgentAspect>();
        _explorersWithTarget = World.Filter.
            Extend<MobileAgentAspect>().
            With<ExplorerComponent>().
            With<TargetObserverComponent>().
            Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach(Entity e in _explorersWithTarget)
        {
            var target = e.GetComponent<TargetObserverComponent>();

            if (target.IsAcquired)
            {
                ref var pursuer = ref e.AddComponent<TargetPursuerComponent>();
                pursuer.preserved = e.GetComponent<ExplorerComponent>();
                e.RemoveComponent<ExplorerComponent>();
            }
        }
    }
}