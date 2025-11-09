using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAttackAnimationController : IInternalAnimationController
{
    private Animator _animator;
    private List<int> _movParamHashes = new();
    private int _attackTriggerHash;
    private int _attackSwitchHash;
    private int _numberOfAttacks = 0;

    public WalkAttackAnimationController(AnimationConfig cfg, Animator animator)
    {
        _animator = animator;
        foreach (string param in cfg.WalkDimensionNames)
            _movParamHashes.Add(Animator.StringToHash(param));
        _attackTriggerHash = Animator.StringToHash(cfg.AttackTriggerName);
        _attackSwitchHash = Animator.StringToHash(cfg.AttackSwitchName);
        _numberOfAttacks = cfg.NumberOfAttacks;
    }

    public void SetWalkDirection(List<float> dir)
    {
        if (dir.EuclideanNorm() > 1f)
            dir.EuqlideanNormalize(5e-3f);

        int dimensions = Mathf.Min(dir.Count, _movParamHashes.Count);
        for (int i = 0; i < dimensions; i++)
            _animator.SetFloat(_movParamHashes[i], dir[i]);
    }

    public void TriggerAttack(int id)
    {
        if (id >= _numberOfAttacks)
            return;

        _animator.SetInteger(_attackSwitchHash, id);
        _animator.SetTrigger(_attackTriggerHash);
    }
}
