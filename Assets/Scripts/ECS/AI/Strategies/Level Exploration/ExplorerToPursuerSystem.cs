using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class ExplorerToPursuerSystem : CustomUpdateSystem
{
    AspectFactory<AgentAspect> _agentFactory;
    Filter _explorersWithTarget;

    public override void OnAwake()
    {
        _agentFactory = World.GetAspectFactory<AgentAspect>();
        _explorersWithTarget = World.Filter.
            Extend<AgentAspect>().
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