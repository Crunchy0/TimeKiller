using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class MovementSystem : CustomFixedUpdateSystem
{
    Filter _movementFilter;
    Vector3 _dir = default;

    public override void OnAwake() {
        _movementFilter = World.Filter.With<BodyComponent>().With<MovementComponent>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(Entity e in _movementFilter)
        {
            var movComp = e.GetComponent<MovementComponent>();
            _dir.x = movComp.direction.x;
            _dir.z = movComp.direction.y;

            var body = e.GetComponent<BodyComponent>();
            Vector3 force = _dir * movComp.speed * deltaTime;
            if (body.rigidbody.velocity.magnitude < movComp.speed)
                body.rigidbody.AddForce(force, ForceMode.VelocityChange);
            //Debug.Log($"Movement direction ({bodyComp.transform.name}): {movComp.direction}");
        }
    }
}