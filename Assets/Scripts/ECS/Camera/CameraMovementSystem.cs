using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(CameraMovementSystem))]
public sealed class CameraMovementSystem : LateUpdateSystem {
    CameraController _camController;

    public override void OnAwake()
    {
        
    }

    public override void OnUpdate(float deltaTime)
    {
        CameraConfig config = _camController.Config;
        Transform camTf = _camController.CameraTransform;
        Vector3 dest = _camController.CameraDestination;

        Vector3 targetShift = dest - camTf.position;

        if (targetShift.magnitude > config.FollowTh)
            _camController.IsCurrentlyFollowing = true;
        else if (targetShift.magnitude < 0.1f)
            _camController.IsCurrentlyFollowing = false;

        if (!_camController.IsCurrentlyFollowing)
            return;

        float speed = config.BaseSpeed + config.Acceleration * targetShift.magnitude;
        camTf.position += targetShift.normalized * speed * deltaTime;
    }

    [Inject]
    private void InjectDependencies(CameraController camController)
    {
        _camController = camController;
    }
}