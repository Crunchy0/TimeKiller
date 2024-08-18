using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using TriInspector;

public class RootLifetimeScope : LifetimeScope
{
    [Required] [SerializeField] ConfigBase _configBase;

    protected override void Configure(IContainerBuilder builder)
    {
        Controls contr = new Controls();
        contr.Enable();
        builder.RegisterInstance(contr);

        builder.RegisterComponent(_configBase);
    }
}
