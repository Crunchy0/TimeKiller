using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CameraSetup
{
    public Transform CameraTransform { get; private set; }
    public Transform TargetTransform { get; private set; }

    public CameraSetup(Transform camTf, Transform targTf)
    {
        CameraTransform = camTf;
        TargetTransform = targTf;
    }
}
