using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(OrientationSystem))]
public sealed class OrientationSystem : UpdateSystem {
    Filter _rotFilter;

    public override void OnAwake() {
        _rotFilter = World.Filter.With<BodyComponent>().With<ActorComponent>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(Entity e in _rotFilter)
        {
            var bodyComp = e.GetComponent<BodyComponent>();
            var actorComp = e.GetComponent<ActorComponent>();

            Vector3 targetPoint = actorComp.lookTarget;
            targetPoint.y = bodyComp.transform.position.y;

            bodyComp.transform.LookAt(targetPoint);
        }
    }
}