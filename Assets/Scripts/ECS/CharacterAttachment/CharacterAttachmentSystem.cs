using Scellecs.Morpeh.Providers;
using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(CharacterAttachmentSystem))]
public sealed class CharacterAttachmentSystem : UpdateSystem {
    Controls _controls;
    ICameraMonitor _camMonitor;
    PlayerControlledEntity _servInfo;
    Event<PickCharacterEvent> _pickEvt;
    Event<AttachedToCharacterEvent> _attachEvt;

    public override void OnAwake()
    {
        _pickEvt = World.GetEvent<PickCharacterEvent>();
        _attachEvt = World.GetEvent<AttachedToCharacterEvent>();
    }

    public override void OnUpdate(float deltaTime)
    {
        if (_controls.Interactions.DetachFromEntity.WasReleasedThisFrame())
        {
            _servInfo.ResetServant();
            _attachEvt.NextFrame(new AttachedToCharacterEvent
            {
                controlledId = _servInfo.ControlledId,
                controlledTransform = _servInfo.Controlled.transform
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

        _servInfo.ResetServant(gameObject);
        _attachEvt.NextFrame(new AttachedToCharacterEvent
        {
            controlledId = _servInfo.ControlledId,
            controlledTransform = _servInfo.Controlled.transform
        });
    }

    [Inject]
    private void InjectDependencies(Controls controls, PlayerControlledEntity servInfo, ICameraMonitor camMonitor)
    {
        _camMonitor = camMonitor;
        _controls = controls;
        _servInfo = servInfo;
    }
}