using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(ControlsInitializer))]
public sealed class ControlsInitializer : Initializer {
    private Controls _controls;

    public override void OnAwake() {
        _controls.Enable();
    }

    public override void Dispose() {
    }

    [Inject]
    private void InjectControls(Controls controls)
    {
        _controls = controls;
    }
}