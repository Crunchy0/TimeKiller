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

        builder.Register<PlayerControlledEntity>(Lifetime.Scoped).WithParameter("picker", _characterPicker);

        builder.Register<GroupRelationMatrix>(Lifetime.Scoped);

        CameraSetup setup = new(_cameraTransform, _characterPicker.transform);
        builder.Register<CameraController>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf().WithParameter("setup", setup);
    }
}
