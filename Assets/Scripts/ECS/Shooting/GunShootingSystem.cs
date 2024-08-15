using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class GunShootingSystem : CustomUpdateSystem
{
    private Request<SpawnBulletRequest> _spawnBulletRequest;
    private Filter _gunFilter;

    public override void OnAwake()
    {
        _spawnBulletRequest = World.GetRequest<SpawnBulletRequest>();
        _gunFilter = World.Filter.With<GunComponent>().With<BulletSpawner>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (Entity e in _gunFilter)
        {
            ref var gunComp = ref e.GetComponent<GunComponent>();

            float curTime = Time.time;
            if (curTime < gunComp.lastShotTime + gunComp.config.TimeBetweenShots)
                continue;

            bool allowedToShoot =
                gunComp.isTriggerPulled &&
                gunComp.ammoLeft > 0 &&
                (gunComp.isAutoEnabled || !gunComp.shotOnce);
            if (allowedToShoot)
            {
                _spawnBulletRequest.Publish(new SpawnBulletRequest { targetEntityId = e.ID });
                gunComp.lastShotTime = curTime;
                gunComp.shotOnce = true;
            }
        }
    }
}