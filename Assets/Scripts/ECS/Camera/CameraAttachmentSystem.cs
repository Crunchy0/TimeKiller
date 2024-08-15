using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class CameraAttachmentSystem : CustomUpdateSystem {
    CameraController _camController;
    Event<AttachedToCharacterEvent> _attachEvt;

    public CameraAttachmentSystem(CameraController camController) => _camController = camController;

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
}