using Scellecs.Morpeh;
using System.Collections.Generic;

public class HealthSystemsFactory : ISystemsGroupFactory
{
    public SystemsGroup Create(World world)
    {
        List<ISystem> systems = new List<ISystem>
        {
            new PassiveRegenerationSystem(),
            new ApplyDamageSystem(),
            new HandleDeathSystem(),
            new TargetResetOnDeathSystem()
        };

        var group = world.CreateSystemsGroup();
        foreach (var sys in systems)
            group.AddSystem(sys);
        return group;
    }
}
