using Scellecs.Morpeh.Providers;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public abstract class GunProvider : EntityProvider {
    protected GunConfig _config = null;
    protected IEntityStateFactory _factory = null;

    protected sealed override void Initialize()
    {
        ref var gunComp = ref Entity.AddComponent<GunComponent>();
        gunComp.config = _config;

        ref var stateComp = ref Entity.AddComponent<EquipmentState>();
        stateComp.factory = _factory;
        // FOR TESTING - I HAVEN'T DECIDED YET HOW TO HANDLE EQUIPMENT
        // stateComp.state = _factory.CreateState();

        ref var equipmentComp = ref Entity.AddComponent<Equipment>();
        equipmentComp.main = MainAttackStart;
        equipmentComp.mainStop = MainAttackStop;
        equipmentComp.alt = AltAttackStart;
        equipmentComp.altStop = AltAttackStop;
    }

    protected sealed override void Deinitialize()
    {
        Entity.RemoveComponent<GunComponent>();
        Entity.RemoveComponent<EquipmentState>();
    }

    [Inject]
    protected abstract void Setup(IObjectResolver resolver);

    protected virtual void MainAttackStart(World world)
    {
        var pullTriggerReq = world.GetRequest<PullGunTrigger>();
        pullTriggerReq.Publish(new PullGunTrigger { isPulled = true, entityId = Entity.ID });
    }

    protected virtual void MainAttackStop(World world)
    {
        var pullTriggerReq = world.GetRequest<PullGunTrigger>();
        pullTriggerReq.Publish(new PullGunTrigger { isPulled = false, entityId = Entity.ID });
    }

    protected virtual void AltAttackStart(World world) { }

    protected virtual void AltAttackStop(World world) { }
}