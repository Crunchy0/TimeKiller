using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class PlayerMovement : CustomUpdateSystem
{
    Controls _controls;
    PlayerControlledEntity _controlled;
    ICameraMonitor _camMonitor;

    public PlayerMovement(
        Controls controls,
        PlayerControlledEntity controlled,
        ICameraMonitor camMonitor) =>
        (_controls, _controlled, _camMonitor) = (controls, controlled, camMonitor);

    public override void OnAwake()
    {
        //_movFilter = World.Filter.With<PlayerComponent>().With<CreatureMovement>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        if (!World.TryGetEntity(_controlled.Id, out Entity e) || !e.Has<MovementComponent>())
            return;

        ref var movComp = ref e.GetComponent<MovementComponent>();

        Transform camTf = _camMonitor.CameraTransform;
        Vector2 movDir = _controls.Movement.Move.ReadValue<Vector2>();
        Vector3 fwd = (camTf.forward + camTf.up);
        fwd.y = 0f;
        fwd.Normalize();

        Vector3 resultDir = movDir.x * camTf.right + movDir.y * fwd;
        movComp.direction.x = resultDir.x;
        movComp.direction.y = resultDir.z;
    }
}