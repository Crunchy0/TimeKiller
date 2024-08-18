using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class BulletSpawnSystem : CustomUpdateSystem
{
    Request<SpawnBulletRequest> _spawnBulletReq;
    Request<TakeDamageRequest> _damageReq;

    public override void OnAwake() {
        _spawnBulletReq = World.GetRequest<SpawnBulletRequest>();
        _damageReq = World.GetRequest<TakeDamageRequest>();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(var req in _spawnBulletReq.Consume())
        {
            Entity e;
            if (World.TryGetEntity(req.targetEntityId, out e) && e.Has<BulletSpawner>())
            {
                var bsComp = e.GetComponent<BulletSpawner>();
                SpawnBullet(bsComp);
            }
        }
    }

    private void SpawnBullet(BulletSpawner bsComp)
    {
        Transform tf = bsComp.spawnPoint;
        var ray = new Ray(tf.position, tf.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 50f, LayerMask.GetMask("Population", "Level", "Default")))
        {
            //Debug.Log($"Bullet hit some surface at {hit.point}");
            Quaternion rot = Quaternion.FromToRotation(bsComp.bulletMarkPrefab.transform.up, hit.normal);
            
            var gameObject = GameObject.Instantiate(bsComp.bulletMarkPrefab, hit.point + hit.normal*0.01f, rot);
            gameObject.transform.SetParent(hit.transform);

            var provider = hit.transform.GetComponentInParent<EntityProvider>();
            if (provider == null)
                return;

            _damageReq.Publish(new TakeDamageRequest { targetId = provider.Entity.ID, damage = bsComp.damage });
        }
    }
}