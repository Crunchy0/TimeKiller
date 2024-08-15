using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class PlayerActions : CustomUpdateSystem
{
    private Event<PrimaryActionEvent> _primActEvent;
    private Event<SecondaryActionEvent> _secActEvent;
    private Controls _controls;
    private PlayerControlledEntity _controlled;

    public PlayerActions(
        Controls controls,
        PlayerControlledEntity controlled) =>
        (_controls, _controlled) = (controls, controlled);

    public override void OnAwake() {
        _primActEvent = World.GetEvent<PrimaryActionEvent>();
        _secActEvent = World.GetEvent<SecondaryActionEvent>();
    }

    public override void OnUpdate(float deltaTime) {
        if (_controls.Interactions.PimaryFire.WasPerformedThisFrame())
            _primActEvent.NextFrame(new PrimaryActionEvent { actorId = _controlled.Id, activated = true });

        if (_controls.Interactions.PimaryFire.WasReleasedThisFrame())
            _primActEvent.NextFrame(new PrimaryActionEvent { actorId = _controlled.Id, activated = false });

        if (_controls.Interactions.SecondaryFire.WasPerformedThisFrame())
            _secActEvent.NextFrame(new SecondaryActionEvent { actorId = _controlled.Id, activated = true });

        if (_controls.Interactions.SecondaryFire.WasReleasedThisFrame())
            _secActEvent.NextFrame(new SecondaryActionEvent { actorId = _controlled.Id, activated = false });
    }
}