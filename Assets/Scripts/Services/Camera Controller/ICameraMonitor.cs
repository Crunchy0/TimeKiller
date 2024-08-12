using UnityEngine;

public interface ICameraMonitor
{
    public CameraConfig Config { get; }
    public Camera Camera { get; }
    public Transform TargetTransform { get; }
    public Transform CameraTransform { get; }
}
