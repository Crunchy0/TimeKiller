using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(MovementSystem))]
public sealed class MovementSystem : FixedUpdateSystem {
    private Filter _movementFilter;

    public override void OnAwake() {
        _movementFilter = World.Filter.With<BodyComponent>().With<MovementComponent>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(Entity e in _movementFilter)
        {
            var movComp = e.GetComponent<MovementComponent>();
            float x = movComp.direction.x;
            float z = movComp.direction.y;
            Vector3 dir = new Vector3(x, 0f, z) * movComp.speed * deltaTime;

            var bodyComp = e.GetComponent<BodyComponent>();
            if (bodyComp.rigidbody.velocity.magnitude < movComp.speed)
                bodyComp.rigidbody.AddForce(dir, ForceMode.VelocityChange);

            //Debug.Log($"Movement direction ({bodyComp.transform.name}): {movComp.direction}");
        }
    }
}