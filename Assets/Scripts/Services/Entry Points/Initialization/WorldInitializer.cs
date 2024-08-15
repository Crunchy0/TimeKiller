using Scellecs.Morpeh;
using System.Collections.Generic;
using VContainer;

public class WorldInitializer
{
    [Inject] AiNavigationSystemsFactory _aiNavFactory;
    [Inject] CameraSystemsFactory _camFactory;
    [Inject] CharacterAttachmentSystemsFactory _attachFactory;
    [Inject] CharacterOrientationSystemsFactory _orientFactory;
    [Inject] EquipmentSystemsFactory _equipmentFactory;
    [Inject] PlayerInputSystemsFactory _inputFactory;
    [Inject] ShootingSystemsFactory _shootingFactory;
    [Inject] StrategySystemsFactory _stratFactory;
    [Inject] StrategyTransitionSystemsFactory _stratTransFactory;
    [Inject] TargetTrackingSystemsFactory _targTrackFactory;
    [Inject] MiscSystemsFactory _miscFactory;

    public void Initialize()
    {
        var groupFactories = new List<ISystemsGroupFactory>
        {
            _aiNavFactory, _camFactory, _attachFactory, _orientFactory,
            _equipmentFactory, _inputFactory, _shootingFactory, _stratFactory,
            _stratTransFactory, _targTrackFactory, _miscFactory
        };

        for (int i = 0; i < groupFactories.Count; i++)
        {
            var group = groupFactories[i].Create(World.Default);
            World.Default.AddSystemsGroup(i, group);
        }
    }
}
