using Scellecs.Morpeh;
using System.Collections.Generic;

public class ShootingSystemsFactory : ISystemsGroupFactory
{
    public SystemsGroup Create(World world)
    {
        List<ISystem> systems = new List<ISystem>
        {
            new GunPullTriggerSystem(),
            new GunShootingSystem(),
            new BulletSpawnSystem()
        };

        var group = world.CreateSystemsGroup();
        foreach (var sys in systems)
            group.AddSystem(sys);
        return group;
    }
}
