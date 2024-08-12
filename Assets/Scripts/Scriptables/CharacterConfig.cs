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

    public float MaxHp { get => _maxHp; }
    public float Regen { get => _regeneration; }

    public float MovSpeed { get => _movementSpeed; }
    public float SightRange { get => _sightRange; }
    public Vector2 Fov { get => _fov; }
    public float LostTargetPursuitDuration { get => _lostTargetPursuitDuration; }
    public float ExplorationPenchant { get => _explorationPenchant; }

    public float AttackRange { get => _attackRange; }
    public float AttackSpan { get => _attackSpan; }
    public float AttackCooldown { get => _attackCooldown; }

    [Header("General")]
    [SerializeField] private CharacterGroupId _groupId;

    [Header("Health")]
    [SerializeField] [Min(0.01f)] private float _maxHp;
    [SerializeField] [Min(0f)] private float _regeneration;

    [Header("Navigation")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] [Min(0f)] private float _sightRange;
    [SerializeField] private Vector2 _fov;
    [SerializeField] [Min(0.2f)] private float _lostTargetPursuitDuration;
    [SerializeField] [Range(0f, 1f)] private float _explorationPenchant;

    [Header("Combat")]
    [SerializeField] [Min(0f)] private float _attackRange;
    [SerializeField] [Min(0.1f)] private float _attackSpan;
    [SerializeField] [Min(0.1f)] private float _attackCooldown;
}
