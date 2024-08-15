using Scellecs.Morpeh;
using System.Collections.Generic;

public class AiNavigationSystemsFactory : ISystemsGroupFactory
{
    GroupRelationMatrix _relMatrix;

    public AiNavigationSystemsFactory(GroupRelationMatrix relMatrix)
    {
        _relMatrix = relMatrix;
    }

    public SystemsGroup Create(World world)
    {
        List<ISystem> systems = new List<ISystem>
        {
            new AiNavigationSystem(_relMatrix)
        };

        var group = world.CreateSystemsGroup();
        foreach (var sys in systems)
            group.AddSystem(sys);
        return group;
    }
}
