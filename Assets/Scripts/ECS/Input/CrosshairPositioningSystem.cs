using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(CrosshairPositioningSystem))]
public sealed class CrosshairPositioningSystem : UpdateSystem {
    ICameraMonitor _camMonitor;
    Controls _controls;
    PlayerControlledEntity _servInfo;
    TargetInfo _targInfo;
    LayerMask _surfaceMask;

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

    [Inject]
    private void InjectDependencies(TargetInfo targInfo, Controls controls, PlayerControlledEntity servInfo, ICameraMonitor camMonitor)
    {
        _camMonitor = camMonitor;
        _targInfo = targInfo;
        _controls = controls;
        _servInfo = servInfo;
    }
}