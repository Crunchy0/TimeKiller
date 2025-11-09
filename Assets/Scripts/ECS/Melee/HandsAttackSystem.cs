using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class HandsAttackSystem : CustomUpdateSystem {
    Filter _hands;
    Request<TakeDamageRequest> _damageReq;

    public override void OnAwake()
    {
        _hands = World.Filter.With<MonsterHandsComponent>().Build();
        _damageReq = World.GetRequest<TakeDamageRequest>();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (Entity e in _hands)
        {
            ref var hands = ref e.GetComponent<MonsterHandsComponent>();

            if (!hands.isTriggered)
                continue;

            ApplyDamage(ref hands, e.ID);

            hands.isTriggered = false;
        }
    }

    private void ApplyDamage(ref MonsterHandsComponent hands, EntityId selfId)
    {
        // TODO: Add specific case checking - the character mustn't be able to damage itself

        foreach(Collider col in Physics.OverlapSphere(hands.attackSpot.position, hands.attackRadius, LayerMask.GetMask("Population")))
        {
            var provider = col.gameObject.GetComponentInParent<HealthProvider>();
            if (provider == null)
                continue;

            Entity e = provider.Entity;
            if(e.Has<ActiveEquipment>())
            {
                var activeEq = e.GetComponent<ActiveEquipment>();
                if (activeEq.equippedId == selfId)
                    continue;
            }

            _damageReq.Publish(new TakeDamageRequest { targetId = provider.Entity.ID, damage = hands.damage });
        }
    }
}