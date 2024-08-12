using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using TriInspector;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class BodyProvider : MonoProvider<BodyComponent> {
    [SerializeField] [Required] Rigidbody _rigidbody;

    protected override void Initialize()
    {
        ref var bodyComp = ref GetData();
        bodyComp.transform = transform;
        bodyComp.rigidbody = _rigidbody;
    }
}