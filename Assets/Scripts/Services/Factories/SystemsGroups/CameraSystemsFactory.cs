using Scellecs.Morpeh;
using System.Collections.Generic;

public class CameraSystemsFactory : ISystemsGroupFactory
{
    private CameraController _camController;

    public CameraSystemsFactory(CameraController camController)
    {
        _camController = camController;
    }

    public SystemsGroup Create(World world)
    {
        List<ISystem> systems = new List<ISystem>
        {
            new CameraCachingSystem(_camController),
            new CameraAttachmentSystem(_camController),
            new CameraPositioningSystem(_camController)
        };

        var group = world.CreateSystemsGroup();
        foreach (var sys in systems)
            group.AddSystem(sys);
        return group;
    }
}
