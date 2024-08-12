using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class ShotgunProvider : GunProvider {

    [Inject]
    protected sealed override void Setup(IObjectResolver resolver)
    {
        _config = resolver.Resolve<ConfigBase>().ShotgunConfig;
        _factory = resolver.Resolve<ShotgunStateFactory>();
    }
}