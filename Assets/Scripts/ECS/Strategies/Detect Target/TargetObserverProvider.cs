using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class TargetObserverProvider : MonoProvider<TargetObserverComponent> {
    protected override void Initialize()
    {
        ref var target = ref GetData();
        target.Forget();
    }
}