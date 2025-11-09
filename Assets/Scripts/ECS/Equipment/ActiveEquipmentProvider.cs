using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class ActiveEquipmentProvider : EntityProvider {
    [SerializeField] private GameObject _equipmentPrefab;
    [SerializeField] private Transform _equipmentLocation;

    protected override void Initialize()
    {
        if (!_equipmentPrefab.TryGetComponent<EntityProvider>(out var provider))
            return;

        ref var activeEq = ref Entity.AddComponent<ActiveEquipment>();
        var gameObject = Instantiate(_equipmentPrefab, _equipmentLocation.position, _equipmentLocation.rotation, _equipmentLocation);
        activeEq.gameObject = gameObject;
        activeEq.equippedId = gameObject.GetComponent<EntityProvider>().Entity.ID;
    }

    // (animation <-> logic) attack signals
    private void UseEquipment(int code)
    {
        bool activated = false;
        switch (code)
        {
            case 0:
                break;
            case 1:
                activated = true;
                break;
            default:
                return;
        }

        SendUseEvent(activated);
    }

    private void SendUseEvent(bool activated)
    {
        var useEvt = World.Default.GetEvent<PrimaryActionEvent>();
        useEvt.NextFrame(new PrimaryActionEvent
        {
            actorId = Entity.ID,
            activated = activated
        });
    }
}