using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LevelLifetimeScope : LifetimeScope
{
    [SerializeField] private Transform _crosshairPlane;
    [SerializeField] private Transform _origin;
    [SerializeField] private GameObject _characterPicker;
    [SerializeField] private Transform _cameraTransform;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<TargetInfo>(Lifetime.Scoped).WithParameter("origin", _origin).WithParameter("chPlane", _crosshairPlane);

        builder.Register<PlayerControlledEntity>(Lifetime.Scoped).WithParameter("defaultObject", _characterPicker);

        builder.Register<GroupRelationMatrix>(Lifetime.Scoped);

        CameraSetup setup = new(_cameraTransform, _characterPicker.transform);
        builder.Register<CameraController>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf().WithParameter("setup", setup);

        builder.Register<AiNavigationSystemsFactory>(Lifetime.Scoped);
        builder.Register<CameraSystemsFactory>(Lifetime.Scoped);
        builder.Register<CharacterAttachmentSystemsFactory>(Lifetime.Scoped);
        builder.Register<CharacterOrientationSystemsFactory>(Lifetime.Scoped);
        builder.Register<EquipmentSystemsFactory>(Lifetime.Scoped);
        builder.Register<PlayerInputSystemsFactory>(Lifetime.Scoped);
        builder.Register<ShootingSystemsFactory>(Lifetime.Scoped);
        builder.Register<StrategySystemsFactory>(Lifetime.Scoped);
        builder.Register<StrategyTransitionSystemsFactory>(Lifetime.Scoped);
        builder.Register<TargetTrackingSystemsFactory>(Lifetime.Scoped);
        builder.Register<MiscSystemsFactory>(Lifetime.Scoped);

        builder.Register<WorldInitializer>(Lifetime.Scoped);
    }
}
