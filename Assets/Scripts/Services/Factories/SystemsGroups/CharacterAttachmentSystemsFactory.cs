using Scellecs.Morpeh;
using System.Collections.Generic;

public class CharacterAttachmentSystemsFactory : ISystemsGroupFactory
{
    Controls _controls;
    PlayerControlledEntity _controlled;
    ICameraMonitor _camMonitor;

    public CharacterAttachmentSystemsFactory(
        Controls controls,
        PlayerControlledEntity controlled,
        ICameraMonitor camMonitor)
    {
        _controls = controls;
        _controlled = controlled;
        _camMonitor = camMonitor;
    }

    public SystemsGroup Create(World world)
    {
        List<ISystem> systems = new List<ISystem>
        {
            new CharacterAttachmentSystem(_controls, _controlled, _camMonitor),
            new PrepareControlledNpcSystem()
        };

        var group = world.CreateSystemsGroup();
        foreach (var sys in systems)
            group.AddSystem(sys);
        return group;
    }
}
