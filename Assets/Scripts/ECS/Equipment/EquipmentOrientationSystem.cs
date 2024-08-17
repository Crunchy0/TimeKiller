using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class EquipmentOrientationSystem : CustomUpdateSystem
{
    Filter _eqFilter;

    public override void OnAwake() {
        _eqFilter = World.Filter.With<ActorComponent>().With<ActiveEquipment>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(Entity e in _eqFilter)
        {
            var actorComp = e.GetComponent<ActorComponent>();
            var activeEqComp = e.GetComponent<ActiveEquipment>();

            activeEqComp.gameObject.transform.LookAt(actorComp.lookTarget);
        }
    }
}