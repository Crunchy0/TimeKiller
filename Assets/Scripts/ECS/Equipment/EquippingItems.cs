using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using VContainer;
using VContainer.Unity;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(EquippingItems))]
public sealed class EquippingItems : UpdateSystem {
    private IObjectResolver _resolver;
    private Request<EquipItem> _equipItem;

    public override void OnAwake() {
        _equipItem = World.GetRequest<EquipItem>();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(var req in _equipItem.Consume())
        {
            if(World.TryGetEntity(req.actorId, out Entity e) && e.Has<ActorComponent>())
            {
                var invComp = e.GetComponent<CreatureInventory>();
                if (invComp.inventory.Count <= req.idx)
                    continue;

                ItemEntry itemEntry = invComp.inventory[req.idx];
                if (itemEntry.ItemConfig.EquipmentPrefab == null)
                    continue;

                bool hasEquipment = e.Has<ActiveEquipment>();
                if (hasEquipment)
                {
                    var comp = e.GetComponent<ActiveEquipment>();
                    if (comp.inventoryIdx == req.idx)
                        continue;
                    else
                        Destroy(comp.gameObject);
                }
                else
                    e.AddComponent<ActiveEquipment>();

                ref var actorComp = ref e.GetComponent<ActorComponent>();
                ref var eqComp = ref e.GetComponent<ActiveEquipment>();
                eqComp.inventoryIdx = req.idx;
                Equip(itemEntry, actorComp, out eqComp.equippedId, out eqComp.gameObject);
            }
        }
    }

    private void Equip(ItemEntry entry, ActorComponent actorComp, out EntityId id, out GameObject gameObject)
    {
        id = default;
        gameObject = null;

        ItemConfig config = entry.ItemConfig;
        IEntityState state = entry.ItemState;

        // need to position the fresh object and set parent if necessary
        GameObject equipped = _resolver.Instantiate(config.EquipmentPrefab, actorComp.equipmentLocation);//Instantiate(config.EquipmentPrefab);
        //equipped.transform.SetParent(actorComp.equipmentLocation);

        if (equipped.TryGetComponent<EntityProvider>(out var provider))
        {
            if(!provider.Entity.Has<EquipmentState>())
                return;

            ref var stateComp = ref provider.Entity.GetComponent<EquipmentState>();
            if (stateComp.factory == null)
                return;

            if (state == null)
                state = stateComp.factory.CreateState();
            stateComp.state = state;

            id = provider.Entity.ID;
            gameObject = equipped;
        }
    }

    [Inject]
    private void InjectDependencies(IObjectResolver resolver)
    {
        _resolver = resolver;
    }
}