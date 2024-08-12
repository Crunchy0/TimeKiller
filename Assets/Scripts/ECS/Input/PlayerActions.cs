using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerActions))]
public sealed class PlayerActions : UpdateSystem {
    private Event<PrimaryActionEvent> _primActEvent;
    private Event<SecondaryActionEvent> _secActEvent;
    private Controls _controls;
    private PlayerControlledEntity _servInfo;

    public override void OnAwake() {
        _primActEvent = World.GetEvent<PrimaryActionEvent>();
        _secActEvent = World.GetEvent<SecondaryActionEvent>();
    }

    public override void OnUpdate(float deltaTime) {
        if (_controls.Interactions.PimaryFire.WasPerformedThisFrame())
            _primActEvent.NextFrame(new PrimaryActionEvent { actorId = _servInfo.ControlledId, activated = true });

        if (_controls.Interactions.PimaryFire.WasReleasedThisFrame())
            _primActEvent.NextFrame(new PrimaryActionEvent { actorId = _servInfo.ControlledId, activated = false });

        if (_controls.Interactions.SecondaryFire.WasPerformedThisFrame())
            _secActEvent.NextFrame(new SecondaryActionEvent { actorId = _servInfo.ControlledId, activated = true });

        if (_controls.Interactions.SecondaryFire.WasReleasedThisFrame())
            _secActEvent.NextFrame(new SecondaryActionEvent { actorId = _servInfo.ControlledId, activated = false });
    }

    [Inject]
    private void InjectDependencies(Controls controls, PlayerControlledEntity servInfo)
    {
        _controls = controls;
        _servInfo = servInfo;
    }
}