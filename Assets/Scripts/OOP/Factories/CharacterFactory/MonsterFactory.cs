using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory : ICharacterFactory
{
    GameObject _prefab;

    public MonsterFactory(ConfigBase cfgBase)
    {
        _prefab = cfgBase.MonsterPrefab;
    }

    public GameObject Create()
    {
        GameObject gameObject = GameObject.Instantiate(_prefab);
        return gameObject;
    }
}

