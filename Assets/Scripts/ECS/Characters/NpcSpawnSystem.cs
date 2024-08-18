using Scellecs.Morpeh;
using System.Collections.Generic;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class NpcSpawnSystem : CustomUpdateSystem {
    NpcPrototypeStorage _protoStorage;
    Filter _spawners;

    public NpcSpawnSystem(NpcPrototypeStorage protoStorage) => _protoStorage = protoStorage;

    public override void OnAwake()
    {
        _spawners = World.Filter.With<NpcSpawnerComponent>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(Entity e in _spawners)
        {
            ref var spawner = ref e.GetComponent<NpcSpawnerComponent>();
            float curTime = Time.time;

            if(spawner.lastSpawnTime + spawner.spawnInterval < curTime)
            {
                List<GameObject> groupPrefabs;
                if (!_protoStorage.GetGroup(spawner.ownerId, out groupPrefabs))
                    continue;

                int count = groupPrefabs.Count;
                int idx = Random.Range(0, count);

                var freshNpc = GameObject.Instantiate(groupPrefabs[idx]);
                Collider col = freshNpc.GetComponentInChildren<Collider>();
                if (col == null)
                    continue;
                Bounds bounds = col.bounds;
                freshNpc.transform.position = spawner.zone.GetRandomPoint() + (bounds.size.y / 2 + 0.1f) * Vector3.up;
                spawner.lastSpawnTime = curTime;
            }
        }
    }
}