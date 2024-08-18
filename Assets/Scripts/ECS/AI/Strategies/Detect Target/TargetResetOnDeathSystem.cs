using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class TargetResetOnDeathSystem : CustomUpdateSystem {
    AspectFactory<AgentAspect> _agentFactory;
    Filter _targetAwareFilter;

    public override void OnAwake()
    {
        _agentFactory = World.GetAspectFactory<AgentAspect>();
        _targetAwareFilter = World.Filter.
            Extend<AgentAspect>().
            With<TargetObserverComponent>().
            Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach(Entity e in _targetAwareFilter)
        {
            ref var target = ref e.GetComponent<TargetObserverComponent>();
            if (!target.IsAcquired)
                continue;

            bool isEntityThere = World.TryGetEntity(target.id, out Entity targetEntity);
            isEntityThere = isEntityThere && !targetEntity.IsNullOrDisposed();
            if (!isEntityThere)
                target.Forget();
            else if (targetEntity.Has<HealthComponent>())
            {
                var health = targetEntity.GetComponent<HealthComponent>();
                if (health.hp < 1e-3f)
                    target.Forget();
            }
        }
    }
}