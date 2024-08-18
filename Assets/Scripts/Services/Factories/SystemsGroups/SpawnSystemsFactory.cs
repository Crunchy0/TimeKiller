using Scellecs.Morpeh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystemsFactory : ISystemsGroupFactory
{
    NpcPrototypeStorage _protoStorage;

    public SpawnSystemsFactory(NpcPrototypeStorage protoStorage)
    {
        _protoStorage = protoStorage;
    }

    public SystemsGroup Create(World world)
    {
        List<ISystem> systems = new List<ISystem>{
            new SpawnCaptureSystem(),
            new NpcSpawnSystem(_protoStorage)
        };

        var group = world.CreateSystemsGroup();
        foreach (var sys in systems)
            group.AddSystem(sys);
        return group;
    }
}
