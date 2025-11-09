using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAnimationController : IInternalAnimationController
{
    private Animator _animator;
    private List<int> _movParamHashes = new();

    public WalkAnimationController(AnimationConfig cfg, Animator animator)
    {
        _animator = animator;
        foreach (string param in cfg.WalkDimensionNames)
            _movParamHashes.Add(Animator.StringToHash(param));
    }

    public void SetWalkDirection(List<float> dir)
    {
        if (dir.EuclideanNorm() > 1f)
            dir.EuqlideanNormalize(5e-3f);

        int dimensions = Mathf.Min(dir.Count, _movParamHashes.Count);
        for(int i = 0; i < dimensions; i++)
            _animator.SetFloat(_movParamHashes[i], dir[i]);
    }

    public void TriggerAttack(int id) { }
}
