using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class NpcOrientationSystem : CustomUpdateSystem
{
    Filter _rotFilter;
    Vector3 _eulers = new();

    public override void OnAwake() {
        _rotFilter = World.Filter.With<BodyComponent>().With<ActorComponent>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(Entity e in _rotFilter)
        {
            var bodyComp = e.GetComponent<BodyComponent>();
            var actorComp = e.GetComponent<ActorComponent>();

            Transform eye = actorComp.eye;
            float angle = Vector3.SignedAngle(eye.forward, actorComp.lookTarget - eye.position, eye.up);
            if (Mathf.Abs(angle) < 1f)
                continue;

            angle *= Mathf.Deg2Rad;
            float baseSpeed = actorComp.config.BaseAngularSpeed;
            float acceleration = actorComp.config.AngularAcceleration;

            float resultSpeed = (angle > 0 ? 1 : -1) * baseSpeed + angle * acceleration;
            _eulers.y = resultSpeed;
            bodyComp.transform.Rotate(_eulers);
        }
    }
}