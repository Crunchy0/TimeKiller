using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class SpawnZone : Zone
{
    [SerializeField] [Min(5f)] float _spawnInterval;
    Timer _spawnTimer;
    // Owners factory or prefab here
    [SerializeField] CharacterGroupId _ownerId;
    ICharacterFactory _ownerFactory;

    IObjectResolver _resolver;

    public override void DecreasePopulation(CharacterGroupId key)
    {
        base.DecreasePopulation(key);
        // todo: Add changing zone owners on extinction

        if (GetPopulation(_ownerId) > 0)
            return;

        int maxPresent = 0;
        CharacterGroupId maxPresentId = CharacterGroupId.NONE;
        foreach (var groupId in Enum.GetValues(typeof(CharacterGroupId)))
        {
            int pop = GetPopulation((CharacterGroupId)groupId);
            if (pop < 1)
                continue;
            if (pop > maxPresent)
            {
                maxPresent = pop;
                maxPresentId = (CharacterGroupId)groupId;
            }
        }

        if (maxPresentId != CharacterGroupId.NONE)
        {
            _ownerId = maxPresentId;
            SetOwnerGroup(_ownerId);
        }
    }

    private void Awake()
    {
        _spawnTimer = new Timer(_spawnInterval);
        _spawnTimer.Reset();
        SetOwnerGroup(_ownerId);
    }

    private void OnEnable()
    {
        TimerManager.Subscribe(_spawnTimer.Update);
        _spawnTimer.TimerExpired += OnSpawnRequest;
    }

    private void OnDisable()
    {
        TimerManager.Unsubscribe(_spawnTimer.Update);
        _spawnTimer.TimerExpired -= OnSpawnRequest;
    }

    private void OnSpawnRequest()
    {
        if (_ownerFactory == null)
            return;

        Debug.Log($"Tried spawning {_ownerId}");

        GameObject gameObject = _ownerFactory.Create();
        Collider col = gameObject.GetComponentInChildren<Collider>();
        Bounds bounds = col.bounds;
        gameObject.transform.position = GetRandomPoint() + (bounds.size.y / 2 + 0.1f) * Vector3.up;
        _spawnTimer.Reset();
    }

    private void SetOwnerGroup(CharacterGroupId groupId)
    {
        Debug.Log($"Trying to set the owner {groupId}");
        switch(groupId)
        {
            case CharacterGroupId.HUMAN:
                _ownerFactory = _resolver.Resolve<HumanFactory>();
                break;
            case CharacterGroupId.MONSTER:
                _ownerFactory = _resolver.Resolve<MonsterFactory>();
                break;
        }
    }

    [Inject]
    private void InjectDependencies(IObjectResolver resolver)
    {
        _resolver = resolver;
    }
}
