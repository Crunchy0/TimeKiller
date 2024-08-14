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
        var gameObject = Instantiate(_equipmentPrefab, _equipmentLocation.position, Quaternion.identity, _equipmentLocation);
        activeEq.gameObject = gameObject;
        activeEq.equippedId = gameObject.GetComponent<EntityProvider>().Entity.ID;
    }
}