using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(EquipmentOrientationSystem))]
public sealed class EquipmentOrientationSystem : UpdateSystem {
    Filter _eqFilter;

    public override void OnAwake() {
        _eqFilter = World.Filter.With<ActorComponent>().With<ActiveEquipment>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(Entity e in _eqFilter)
        {
            var actorComp = e.GetComponent<ActorComponent>();
            var activeEqComp = e.GetComponent<ActiveEquipment>();

            Transform eqTf = activeEqComp.gameObject.transform;
            Vector3 direction = actorComp.lookTarget - eqTf.position;
            Vector3 proj = Vector3.ProjectOnPlane(direction, Vector3.up);
            float angle = Vector3.Angle(proj, direction) * (direction.y > proj.y ? -1 : 1);
            //float angle = Vector3.SignedAngle(actorComp.target, eqTf.position, Vector3.up);
            //direction = Quaternion.AngleAxis(angle, Vector3.up) * direction;
            eqTf.localRotation = Quaternion.Euler(angle, 0f, 0f);
            //eqTf.LookAt(eqTf.position + direction);
        }
    }
}