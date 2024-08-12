using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using System;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerEquipping))]
public sealed class PlayerEquipping : UpdateSystem {
    private Request<EquipItem> _equipRequest;
    private Controls _controls;
    private PlayerControlledEntity _servInfo;

    public override void OnAwake() {
        _equipRequest = World.GetRequest<EquipItem>();
        _controls.Interactions.Equip.started += OnEquipPress;
    }

    public override void Dispose()
    {
        _controls.Interactions.Equip.started -= OnEquipPress;
    }

    private void OnEquipPress(InputAction.CallbackContext ctx)
    {
        if (int.TryParse(ctx.control.name, out int idx) && idx >= 0 && idx <= 2)
            _equipRequest.Publish(new EquipItem { actorId = _servInfo.ControlledId, idx = idx - 1 });
    }

    public override void OnUpdate(float deltaTime) {

        
    }

    [Inject]
    private void InjectDependencies(Controls controls, PlayerControlledEntity servInfo)
    {
        _controls = controls;
        _servInfo = servInfo;
    }
}