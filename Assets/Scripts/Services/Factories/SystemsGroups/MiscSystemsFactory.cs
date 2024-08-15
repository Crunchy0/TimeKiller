using Scellecs.Morpeh;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscSystemsFactory : ISystemsGroupFactory
{
    public SystemsGroup Create(World world)
    {
        List<ISystem> systems = new List<ISystem> {
            new TimersUpdateSystem(),
            new MovementSystem()
        };

        var group = world.CreateSystemsGroup();
        foreach (var sys in systems)
            group.AddSystem(sys);
        return group;
    }
}
