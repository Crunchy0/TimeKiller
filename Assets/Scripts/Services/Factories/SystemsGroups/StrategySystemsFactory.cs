using Scellecs.Morpeh;
using System.Collections.Generic;

public class StrategySystemsFactory : ISystemsGroupFactory
{
    public SystemsGroup Create(World world)
    {
        List<ISystem> systems = new List<ISystem>
        {
            new ExplorationSystem(),
            new PursueTargetSystem(),
            new AttackTargetSystem(),
        };

        var group = world.CreateSystemsGroup();
        foreach (var sys in systems)
            group.AddSystem(sys);
        return group;
    }
}
