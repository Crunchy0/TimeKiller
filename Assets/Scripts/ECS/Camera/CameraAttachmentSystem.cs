using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(CameraAttachmentSystem))]
public sealed class CameraAttachmentSystem : UpdateSystem {
    CameraController _camController;
    Event<AttachedToCharacterEvent> _attachEvt;

    public override void OnAwake() {
        _attachEvt = World.GetEvent<AttachedToCharacterEvent>();
    }

    public override void OnUpdate(float deltaTime) {
        int idx = _attachEvt.publishedChanges.length - 1;   // Get the last published event
        if (idx < 0)
            return;

        var evt = _attachEvt.publishedChanges.data[idx];
        _camController.TargetTransform = evt.controlledTransform;
    }

    [Inject]
    private void InjectDependencies(CameraController camController)
    {
        _camController = camController;
    }
}