using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInternalAnimationController
{
    public void SetWalkDirection(List<float> dir);
    public void TriggerAttack(int id);
}
