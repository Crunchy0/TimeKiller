using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(CollectingItems))]
public sealed class CollectingItems : UpdateSystem {
    private Request<CollectItem> _collectItem;

    public override void OnAwake() {
        _collectItem = World.GetRequest<CollectItem>();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(var req in _collectItem.Consume())
        {
            Entity collector, item;
            if (!World.TryGetEntity(req.collectorId, out collector) ||
                !World.TryGetEntity(req.itemId, out item))
                continue;

            var invComp = collector.GetComponent<CreatureInventory>();
            var itemComp = item.GetComponent<ItemComponent>();

            if (invComp.inventory.Count < invComp.inventory.Capacity)
            {
                invComp.inventory.Add(new ItemEntry(itemComp.config, itemComp.state));
                Destroy(req.itemObject);
            }
        }
    }
}