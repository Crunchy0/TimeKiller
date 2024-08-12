using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ApplyEquipmentPrimary))]
public sealed class ApplyEquipmentPrimary : UpdateSystem {
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