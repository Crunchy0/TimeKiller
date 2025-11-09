using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class MonsterHandsProvider : EntityProvider {
    [SerializeField] [Min(0)] float _attackRaduis;
    [SerializeField] [Min(0)] float _damage;

    protected override void Initialize()
    {
        ref var equipment = ref Entity.AddComponent<Equipment>();
        equipment.main = MainAttackStart;
        equipment.mainStop = MainAttackStop;
        equipment.alt = AltAttackStart;
        equipment.altStop = AltAttackStop;

        ref var hands = ref Entity.AddComponent<MonsterHandsComponent>();
        hands.attackSpot = transform;
        hands.attackRadius = _attackRaduis;
        hands.damage = _damage;
        hands.isTriggered = false;
    }

    protected override void Deinitialize()
    {
        Entity.RemoveComponent<Equipment>();
        Entity.RemoveComponent<MonsterHandsComponent>();
    }

    private void MainAttackStart(World world)
    {
        Debug.Log("Hit ATTACK!!");
        var req = world.GetRequest<PerformHitAttackRequest>();
        req.Publish(new() { id = Entity.ID });
    }

    private void MainAttackStop(World world) { }

    private void AltAttackStart(World world) { }

    private void AltAttackStop(World world) { }
}