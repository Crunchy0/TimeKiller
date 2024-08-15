using Scellecs.Morpeh;
using System.Collections.Generic;

public class StrategyTransitionSystemsFactory : ISystemsGroupFactory
{
    public SystemsGroup Create(World world)
    {
        List<ISystem> systems = new List<ISystem> {
            new ExplorerToPursuerSystem(),
            new PursuerToExplorerSystem(),
            new FromAttackerSystem(),
            new ToAttackerSystem(),
        };

        var group = world.CreateSystemsGroup();
        foreach (var sys in systems)
            group.AddSystem(sys);
        return group;
    }
}
