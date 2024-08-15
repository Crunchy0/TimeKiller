using Scellecs.Morpeh;
using System;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public abstract class CustomUpdateSystem : ISystem, IInitializer, IDisposable
{
    public World World { get; set; }

    public abstract void OnAwake();
    public abstract void OnUpdate(float deltaTime);
    public virtual void Dispose() { }
}
