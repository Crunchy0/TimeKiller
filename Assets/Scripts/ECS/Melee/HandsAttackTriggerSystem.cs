using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class HandsAttackTriggerSystem : CustomUpdateSystem {
    Request<PerformHitAttackRequest> _hitReq;

    public override void OnAwake()
    {
        _hitReq = World.GetRequest<PerformHitAttackRequest>();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach(var req in _hitReq.Consume())
        {
            if (!World.TryGetEntity(req.id, out Entity e) || e.IsNullOrDisposed())
                continue;

            ref var hands = ref e.GetComponent<MonsterHandsComponent>();
            hands.isTriggered = true;
        }
    }
}