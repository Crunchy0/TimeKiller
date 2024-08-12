using Scellecs.Morpeh.Providers;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public class GunProvider : EntityProvider {
    [SerializeField] private GunConfig _config;

    protected sealed override void Initialize()
    {
        ref var gunComp = ref Entity.AddComponent<GunComponent>();
        gunComp.config = _config;
        gunComp.ammoLeft = _config.MaxAmmo;
        gunComp.isAutoEnabled = _config.IsAutoSupported;
        gunComp.isTriggerPulled = false;
        gunComp.shotOnce = false;
        gunComp.lastShotTime = 0;

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

    private void MainAttackStart(World world)
    {
        var pullTriggerReq = world.GetRequest<PullTriggerRequest>();
        pullTriggerReq.Publish(new PullTriggerRequest { isPulled = true, entityId = Entity.ID });
    }

    private void MainAttackStop(World world)
    {
        var pullTriggerReq = world.GetRequest<PullTriggerRequest>();
        pullTriggerReq.Publish(new PullTriggerRequest { isPulled = false, entityId = Entity.ID });
    }

    private void AltAttackStart(World world) { }

    private void AltAttackStop(World world) { }
}