using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class CharacterAttachmentSystem : CustomUpdateSystem {
    Controls _controls;
    ICameraMonitor _camMonitor;
    PlayerControlledEntity _controlled;
    Event<PickCharacterEvent> _pickEvt;
    Event<AttachedToCharacterEvent> _attachEvt;
    Event<PrimaryActionEvent> _primEvt;

    public CharacterAttachmentSystem(
        Controls controls,
        PlayerControlledEntity controlled,
        ICameraMonitor camMonitor) =>
        (_controls, _controlled, _camMonitor) = (controls, controlled, camMonitor);

    public override void OnAwake()
    {
        _pickEvt = World.GetEvent<PickCharacterEvent>();
        _attachEvt = World.GetEvent<AttachedToCharacterEvent>();
        _primEvt = World.GetEvent<PrimaryActionEvent>();
    }

    public override void OnUpdate(float deltaTime)
    {
        if (_controls.Interactions.DetachFromEntity.WasReleasedThisFrame())
        {
            _controlled.TakeControl();
            _attachEvt.NextFrame(new AttachedToCharacterEvent
            {
                controlledId = _controlled.Id,
                controlledTransform = _controlled.Object.transform
            });
            return;
        }

        if (_pickEvt.publishedChanges.length < 1)
            return;

        Ray ray = _camMonitor.Camera.ScreenPointToRay(_controls.Movement.MouseLook.ReadValue<Vector2>());
        if (!Physics.Raycast(ray, out RaycastHit hit, 50f, LayerMask.GetMask("Population")) || hit.rigidbody == null)
            return;
        // What if rigidbody is not on the entitie'd game object? Try finding in parent?
        var gameObject = hit.rigidbody.gameObject;

        _controlled.TakeControl(gameObject);
        _attachEvt.NextFrame(new AttachedToCharacterEvent
        {
            controlledId = _controlled.Id,
            controlledTransform = _controlled.Object.transform
        });
    }
}