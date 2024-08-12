using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scellecs.Morpeh;
using VContainer;

public class InjectableInstaller : Installer
{
    private IObjectResolver _container;

    [Inject]
    private void InjectContainer(IObjectResolver container)
    {
        _container = container;
    }

    private void Start()
    {
        foreach (var initializer in initializers)
        {
            _container.Inject(initializer);
        }

        foreach (var fixedSystem in fixedUpdateSystems)
        {
            _container.Inject(fixedSystem.System);
        }

        foreach (var updateSystem in updateSystems)
        {
            _container.Inject(updateSystem.System);
        }

        foreach (var lateSystem in lateUpdateSystems)
        {
            _container.Inject(lateSystem.System);
        }

        foreach (var cleanupSystem in cleanupSystems)
        {
            _container.Inject(cleanupSystem.System);
        }
    }
}
