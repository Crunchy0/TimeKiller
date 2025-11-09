using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Animation Config", menuName = "ScriptableObject/Animation Config")]
public class AnimationConfig : ScriptableObject
{
    public List<string> WalkDimensionNames { get => _walkDimensionNames; }
    public string AttackTriggerName { get => _attackTriggerName; }
    public string AttackSwitchName { get => _attackSwitchName; }
    public int NumberOfAttacks { get => _numberOfAttacks; }

    [SerializeField] private List<string> _walkDimensionNames;
    [SerializeField] private string _attackTriggerName;
    [SerializeField] private string _attackSwitchName;
    [SerializeField] private int _numberOfAttacks;
}
