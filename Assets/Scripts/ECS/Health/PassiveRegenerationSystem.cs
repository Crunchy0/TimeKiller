using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class PassiveRegenerationSystem : CustomUpdateSystem {
    Filter _living;

    public override void OnAwake()
    {
        _living = World.Filter.With<HealthComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (Entity e in _living)
        {
            ref var health = ref e.GetComponent<HealthComponent>();
            health.hp = Mathf.Clamp(health.hp + health.regen * deltaTime, 0, health.maxHp);
        }
    }
}