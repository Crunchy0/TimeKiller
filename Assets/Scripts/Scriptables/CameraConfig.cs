using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Camera Config", menuName ="ScriptableObject/Camera Config")]
public class CameraConfig : ScriptableObject
{
    [SerializeField] private float _tiltAngle;
    [SerializeField] private float _rotationAngle;
    [SerializeField] [Min(0f)] private float _distanceToTarget;
    [SerializeField] [Min(0f)] private float _followThreshold;
    [SerializeField] [Min(0.001f)] private float _baseSpeed;
    [SerializeField] [Min(0f)] private float _acceleration;

    public float TiltAngle { get => _tiltAngle; }
    public float RotAngle { get => _rotationAngle; }
    public float DistToTarget { get => _distanceToTarget; }
    public float FollowTh { get => _followThreshold; }
    public float BaseSpeed { get => _baseSpeed; }
    public float Acceleration { get => _acceleration; }
}
