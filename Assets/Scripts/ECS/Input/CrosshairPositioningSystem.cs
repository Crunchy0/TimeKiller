using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class CrosshairPositioningSystem : CustomUpdateSystem
{
    Controls _controls;
    PlayerControlledEntity _controlled;
    TargetInfo _targInfo;
    ICameraMonitor _camMonitor;
    LayerMask _surfaceMask;

    public CrosshairPositioningSystem(
        Controls controls,
        PlayerControlledEntity controlled,
        TargetInfo targInfo,
        ICameraMonitor camMonitor) =>
        (_controls, _controlled, _targInfo, _camMonitor) = (controls, controlled, targInfo, camMonitor);

    public override void OnAwake() {
        _surfaceMask = LayerMask.GetMask("Aux");
    }

    public override void OnUpdate(float deltaTime) {
        // Current state + values for calculations
        Plane plane = new Plane(Vector3.up, -_targInfo.OriginHeight);
        Ray ray = _camMonitor.Camera.ScreenPointToRay(_controls.Movement.MouseLook.ReadValue<Vector2>());
        Vector3 position = _targInfo.CrosshairPlane.position;
        Vector3 normal = _targInfo.ProjectionNormal;
        float distance;

        //bool beyond = false;

        if(plane.Raycast(ray, out distance))
        {
            _targInfo.CrosshairTargPos = ray.GetPoint(distance);
            position = _targInfo.CrosshairTargPos;
        }

        // Does cursor point to a target beyond level's bounds or not?
        /*if (Physics.Raycast(ray, out RaycastHit hit, 100f, _surfaceMask))
        {
            _targInfo.CrosshairTargPos = hit.point;
            position = hit.point;
            normal = hit.normal;
        }
        else if (plane.Raycast(ray, out distance))
        {
            beyond = true;
            _targInfo.CrosshairTargPos = ray.GetPoint(distance);
        }*/

        // If the character looks at an obstacle, focus the crosshair on it
        /*Vector3 direction = _targInfo.CrosshairTargPos - _servInfo.Controlled.transform.position;
        Ray projRay = new Ray(_servInfo.Controlled.transform.position, direction);
        if (Physics.Raycast(projRay, out RaycastHit hit2, direction.magnitude + _targInfo.Border, LayerMask.GetMask("Level", "Default")))
        {
            position = hit2.point;
            normal = hit2.normal;
        }
        else if (beyond)
        {
            position = _targInfo.CrosshairTargPos;
            normal = Vector3.up;
        }*/

        // Update crosshair position and (if necessary) rotation
        /*if (_targInfo.ProjectionNormal != normal)
        {
            _targInfo.ProjectionNormal = normal;
            _targInfo.CrosshairPlane.rotation = Quaternion.FromToRotation(Vector3.up, _targInfo.ProjectionNormal);
        }*/
        _targInfo.CrosshairPlane.position = position + _targInfo.ProjectionNormal * 0.01f;
    }
}