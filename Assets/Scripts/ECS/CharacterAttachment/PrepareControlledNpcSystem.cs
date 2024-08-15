using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class PrepareControlledNpcSystem : CustomUpdateSystem {
    Event<AttachedToCharacterEvent> _attachEvt;
    Event<PrimaryActionEvent> _primEvt;

    public override void OnAwake()
    {
        _attachEvt = World.GetEvent<AttachedToCharacterEvent>();
        _primEvt = World.GetEvent<PrimaryActionEvent>();
    }

    public override void OnUpdate(float deltaTime) {
        int idx = _attachEvt.publishedChanges.length - 1;
        if (idx < 0)
            return;

        var evt = _attachEvt.publishedChanges.data[idx];
        if (!World.TryGetEntity(evt.controlledId, out Entity e) || e.IsNullOrDisposed())
            return;

        if (e.Has<AttackTargetComponent>())
            _primEvt.NextFrame(new PrimaryActionEvent { actorId = e.ID, activated = false });
    }
}