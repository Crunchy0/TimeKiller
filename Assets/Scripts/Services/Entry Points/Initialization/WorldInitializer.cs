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
    [Inject] HealthSystemsFactory _healthFactory;
    [Inject] PlayerInputSystemsFactory _inputFactory;
    [Inject] ShootingSystemsFactory _shootingFactory;
    [Inject] StrategySystemsFactory _stratFactory;
    [Inject] StrategyTransitionSystemsFactory _stratTransFactory;
    [Inject] TargetTrackingSystemsFactory _targTrackFactory;
    [Inject] SpawnSystemsFactory _spawnFactory;
    [Inject] MiscSystemsFactory _miscFactory;

    public void Initialize()
    {
        var groupFactories = new List<ISystemsGroupFactory>
        {
            _inputFactory, _camFactory, _attachFactory, _spawnFactory,
            _aiNavFactory, _targTrackFactory, _orientFactory, _stratFactory,
            _stratTransFactory, _equipmentFactory, _shootingFactory, _healthFactory,
            _miscFactory
        };

        for (int i = 0; i < groupFactories.Count; i++)
        {
            var group = groupFactories[i].Create(World.Default);
            World.Default.AddSystemsGroup(i, group);
        }
    }
}
