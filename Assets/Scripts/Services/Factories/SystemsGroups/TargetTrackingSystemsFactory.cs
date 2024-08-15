using Scellecs.Morpeh;
using System.Collections.Generic;

public class TargetTrackingSystemsFactory : ISystemsGroupFactory
{
    GroupRelationMatrix _relMatrix;

    public TargetTrackingSystemsFactory(GroupRelationMatrix relMatrix)
    {
        _relMatrix = relMatrix;
    }

    public SystemsGroup Create(World world)
    {
        List<ISystem> systems = new List<ISystem>
        {
            new TargetAcquirementSystem(_relMatrix),
            new TargetPreservationSystem()
        };

        var group = world.CreateSystemsGroup();
        foreach (var sys in systems)
            group.AddSystem(sys);
        return group;
    }
}
