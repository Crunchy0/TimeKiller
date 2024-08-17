using Scellecs.Morpeh;
using System.Collections.Generic;

public class CharacterOrientationSystemsFactory : ISystemsGroupFactory
{
    public SystemsGroup Create(World world)
    {
        List<ISystem> systems = new List<ISystem>
        {
            new NpcTargetPositioningSystem(),
            new OrientationSystem()
        };

        var group = world.CreateSystemsGroup();
        foreach (var sys in systems)
            group.AddSystem(sys);
        return group;
    }
}
