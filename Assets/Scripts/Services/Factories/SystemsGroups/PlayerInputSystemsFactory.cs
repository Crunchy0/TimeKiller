using Scellecs.Morpeh;
using System.Collections.Generic;

public class PlayerInputSystemsFactory : ISystemsGroupFactory
{
    Controls _controls;
    PlayerControlledEntity _controlled;
    TargetInfo _targInfo;
    ICameraMonitor _camMonitor;

    public PlayerInputSystemsFactory(
        Controls controls,
        PlayerControlledEntity controlled,
        TargetInfo targInfo,
        ICameraMonitor camMonitor)
    {
        _controls = controls;
        _controlled = controlled;
        _targInfo = targInfo;
        _camMonitor = camMonitor;
    }

    public SystemsGroup Create(World world)
    {
        List<ISystem> systems = new List<ISystem>
        {
            new CrosshairPositioningSystem(_controls, _controlled, _targInfo, _camMonitor),
            new PlayerMovement(_controls, _controlled, _camMonitor),
            new PlayerActions(_controls, _controlled),
            new PlayerOrientationSystem(_targInfo, _controlled)
        };

        var group = world.CreateSystemsGroup();
        foreach (var sys in systems)
            group.AddSystem(sys);
        return group;
    }
}
