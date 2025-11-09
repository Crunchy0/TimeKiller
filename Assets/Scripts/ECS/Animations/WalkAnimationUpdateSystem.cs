using Scellecs.Morpeh;
using System.Collections.Generic;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class WalkAnimationUpdateSystem : CustomLateUpdateSystem {
    Filter _animated;
    Vector2 _dir = default;
    List<float> _listDir = new List<float> { 0f, 0f };

    public override void OnAwake()
    {
        _animated = World.Filter.
            With<BodyComponent>().
            With<MovementComponent>().
            With<AnimatedCharacterComponent>().
            Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(Entity e in _animated)
        {
            var body = e.GetComponent<BodyComponent>();
            var movement = e.GetComponent<MovementComponent>();
            var animated = e.GetComponent<AnimatedCharacterComponent>();

            Debug.DrawLine(body.transform.position, body.transform.position + body.transform.forward * 5f, Color.cyan);

            float angle = Mathf.Deg2Rad * Vector3.SignedAngle(body.transform.forward, body.rigidbody.velocity, Vector3.up);

            float speedCoef = body.rigidbody.velocity.magnitude / movement.speed;
            _dir.x = Mathf.Sin(angle) * speedCoef;
            _dir.y = Mathf.Cos(angle) * speedCoef;

            _listDir[0] = _dir.x;
            _listDir[1] = _dir.y;
            animated.controller.SetWalkDirection(_listDir);
        }
    }
}