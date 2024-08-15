using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class ApplyEquipmentPrimary : CustomUpdateSystem {
    private Event<PrimaryActionEvent> _primActEvent;

    public override void OnAwake() {
        _primActEvent = World.GetEvent<PrimaryActionEvent>();
    }

    public override void OnUpdate(float deltaTime) {
        foreach (var evt in _primActEvent.publishedChanges)
        {
            TryApply(evt);
        }
    }

    private void TryApply(PrimaryActionEvent evt)
    {
        Entity e;
        if (!World.TryGetEntity(evt.actorId, out e) || !e.Has<ActiveEquipment>())
            return;

        var activeEq = e.GetComponent<ActiveEquipment>();

        if (World.TryGetEntity(activeEq.equippedId, out e) && e.Has<Equipment>())
        {
            var eqComp = e.GetComponent<Equipment>();
            if (evt.activated)
                eqComp.main(World);
            else
                eqComp.mainStop(World);
        }
    }
}