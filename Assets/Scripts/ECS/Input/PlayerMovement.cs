using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerMovement))]
public sealed class PlayerMovement : UpdateSystem {
    Filter _camFilter;//, _movFilter;
    Controls _controls;
    PlayerControlledEntity _servInfo;
    ICameraMonitor _camMonitor;

    public override void OnAwake()
    {
        //_movFilter = World.Filter.With<PlayerComponent>().With<CreatureMovement>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        if (!World.TryGetEntity(_servInfo.ControlledId, out Entity e) || !e.Has<MovementComponent>())
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

    [Inject]
    private void InjectDependencies(Controls controls, PlayerControlledEntity servInfo, ICameraMonitor camMonitor)
    {
        _camMonitor = camMonitor;
        _controls = controls;
        _servInfo = servInfo;
    }
}