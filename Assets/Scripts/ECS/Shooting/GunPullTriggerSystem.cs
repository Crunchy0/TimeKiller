using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class GunPullTriggerSystem : CustomUpdateSystem
{
    private Request<PullTriggerRequest> _pullTrigger;
    

    public override void OnAwake()
    {
        _pullTrigger = World.GetRequest<PullTriggerRequest>();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(var req in _pullTrigger.Consume())
        {
            if (!World.TryGetEntity(req.entityId, out Entity e) || e.IsNullOrDisposed())
                continue;

            ref var gunComp = ref e.GetComponent<GunComponent>();
            gunComp.isTriggerPulled = req.isPulled;
            if (!gunComp.isTriggerPulled)
                gunComp.shotOnce = false;
        }
    }
}