using Scellecs.Morpeh.Providers;
using Scellecs.Morpeh;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class CreatureInventoryProvider : MonoProvider<CreatureInventory> {
    [SerializeField] List<ItemId> _initialItems;
    Request<CollectItem> _collectItem;

    protected override void Initialize()
    {
        _collectItem = World.Default.GetRequest<CollectItem>();

        ref CreatureInventory comp = ref GetData();
        comp.inventory = new List<ItemEntry>(3);
    }

    private void OnTriggerEnter(Collider other)
    {
        EntityProvider eProvider;
        bool isEntity = other.gameObject.TryGetComponent(out eProvider) && eProvider.Entity != null;
        if (!isEntity)
            return;
        var entity = eProvider.Entity;

        if (!entity.Has<ItemComponent>())
            return;

        _collectItem.Publish(new CollectItem { itemId = entity.ID, collectorId = Entity.ID, itemObject = other.gameObject });
    }
}