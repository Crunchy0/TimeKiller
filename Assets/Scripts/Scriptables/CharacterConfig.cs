using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterGroupId
{
    NONE,
    HUMAN,
    MONSTER
}

[CreateAssetMenu(fileName = "New Character Config", menuName = "ScriptableObject/Character Config")]
public class CharacterConfig : ScriptableObject
{
    public CharacterGroupId GroupId { get => _groupId; }

    public float BaseAngularSpeed { get => _baseAngularSpeed; }
    public float AngularAcceleration { get => _angularAcceleration; }

    public float AttackRange { get => _attackRange; }

    [Header("General")]
    [SerializeField] private CharacterGroupId _groupId;

    [Header("Navigation")]
    [SerializeField] private float _baseAngularSpeed;
    [SerializeField] private float _angularAcceleration;

    [Header("Combat")]
    [SerializeField] [Min(0f)] private float _attackRange;
}
