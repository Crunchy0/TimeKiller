using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(GunShootingSystem))]
public sealed class GunShootingSystem : UpdateSystem
{
    private Request<PullGunTrigger> _pullTrigger;
    private Request<SpawnBulletRequest> _spawnBulletRequest;
    private Filter _gunFilter;

    public override void OnAwake()
    {
        _pullTrigger = World.GetRequest<PullGunTrigger>();
        _spawnBulletRequest = World.GetRequest<SpawnBulletRequest>();
        _gunFilter = World.Filter.With<EquipmentState>().With<GunComponent>().With<BulletSpawner>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var req in _pullTrigger.Consume())
        {
            if (World.TryGetEntity(req.entityId, out Entity e))
            {
                var stateComp = e.GetComponent<EquipmentState>();
                GunState state = (GunState)stateComp.state;
                state.IsTriggerPulled = req.isPulled;
                if (!state.IsTriggerPulled)
                    state.ShotOnce = false;
            }
        }

        foreach (Entity e in _gunFilter)
        {
            var stateComp = e.GetComponent<EquipmentState>();
            var gunComp = e.GetComponent<GunComponent>();
            GunState state = (GunState)stateComp.state;

            if (!state.ShotTimer.IsExpired)
                continue;

            bool allowedToShoot =
                state.IsTriggerPulled &&
                state.AmmoLeft > 0 &&
                (state.IsAutoEnabled || !state.ShotOnce);
            if (allowedToShoot)
            {
                _spawnBulletRequest.Publish(new SpawnBulletRequest { targetEntityId = e.ID });
                state.ResetTimer();
                state.ShotOnce = true;
            }
        }
    }
}