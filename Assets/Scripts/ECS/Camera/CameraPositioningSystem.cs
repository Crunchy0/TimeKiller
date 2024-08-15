using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class CameraPositioningSystem : CustomLateUpdateSystem {
    CameraController _camController;

    public CameraPositioningSystem(CameraController camController) => _camController = camController;

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
}