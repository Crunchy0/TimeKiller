using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInfo
{
    public float OriginHeight { get => _origin.position.y; }
    //public Vector3 OriginPos { get => _origin.position; }
    public Vector3 CrosshairTargPos { get; set; }
    public Vector3 ProjectionNormal { get; set; }
    public Transform CrosshairPlane { get => _chPlane; }
    public float Border { get; private set; }

    private Transform _origin;
    private Transform _chPlane;

    public TargetInfo(Transform origin, Transform chPlane)
    {
        _origin = origin;
        CrosshairTargPos = _origin.position;
        _chPlane = chPlane;
        Border = _chPlane.GetComponent<Renderer>().bounds.size.x / 2f;
    }
}
