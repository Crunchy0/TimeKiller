using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class ApplyDamageSystem : CustomUpdateSystem {
    Request<TakeDamageRequest> _damageReq;

    public override void OnAwake()
    {
        _damageReq = World.GetRequest<TakeDamageRequest>();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(var req in _damageReq.Consume())
        {
            if (!World.TryGetEntity(req.targetId, out Entity e) || e.IsNullOrDisposed() || !e.Has<HealthComponent>())
                continue;

            ref var health = ref e.GetComponent<HealthComponent>();
            health.hp = Mathf.Clamp(health.hp - req.damage, 0, health.maxHp);
        }
    }
}