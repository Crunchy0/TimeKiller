using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanFactory : ICharacterFactory
{
    GameObject _prefab;
    
    public HumanFactory(ConfigBase cfgBase)
    {
        _prefab = cfgBase.HumanPrefab;
    }

    public GameObject Create()
    {
        GameObject gameObject = GameObject.Instantiate(_prefab);
        return gameObject;
    }
}
