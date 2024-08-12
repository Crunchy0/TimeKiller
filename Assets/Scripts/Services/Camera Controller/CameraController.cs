using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : ICameraMonitor
{
    public bool IsCurrentlyFollowing { get; set; } = false;
    public CameraConfig Config { get; private set; }
    public Camera Camera { get => _cam; }
    public Transform TargetTransform { get; set; }
    public Transform CameraTransform
    {
        get => _camTf;
        private set
        {
            if (value.TryGetComponent(out _cam))
                _camTf = value;
        }
    }
    public Vector3 CameraDestination
    {
        get
        {
            Vector3 targetPos = default;
            if (CameraTransform == null)
                return targetPos;
            if (TargetTransform != null)
                targetPos = TargetTransform.position;
            return targetPos + _cachedSt.offset;
        }
    }

    private Transform _camTf = null;
    private Camera _cam = null;
    private CachedCameraState _cachedSt;

    internal struct CachedCameraState
    {
        public float distance;
        public float rotation;
        public float tilt;
        public Vector3 offset;
    }

    public CameraController(ConfigBase configBase, CameraSetup setup)
    {
        Config = configBase.CamConfig;
        CameraTransform = setup.CameraTransform;
        TargetTransform = setup.TargetTransform;
        CacheCurrentState();
    }

    public void CacheCurrentState()
    {
        _cachedSt.distance = Config.DistToTarget;
        _cachedSt.rotation = Config.RotAngle;
        _cachedSt.tilt = Config.TiltAngle;

        RotateCam();
        _cachedSt.offset = -(_cachedSt.distance) * _camTf.forward;
    }

    private void RotateCam()
    {
        _camTf.rotation = Quaternion.Euler(_cachedSt.tilt, _cachedSt.rotation, 0f);
    }
}
