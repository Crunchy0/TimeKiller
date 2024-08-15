using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using TriInspector;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class ActorProvider : MonoProvider<ActorComponent> {
    protected override void Initialize()
    {
        ref var actorComp = ref GetData();
        actorComp.lookTarget = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Entity.IsDisposed() || !other.TryGetComponent<Zone>(out var zone))
            return;

        ref var actorComp = ref GetData();
        if (actorComp.currentZone != null)
            actorComp.currentZone.DecreasePopulation(actorComp.config.GroupId);
        actorComp.currentZone = zone;
        actorComp.currentZone.IncreasePopulation(actorComp.config.GroupId);
    }
}