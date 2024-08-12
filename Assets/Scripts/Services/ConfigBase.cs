using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigBase : MonoBehaviour
{
    public CameraConfig CamConfig { get => _camConfig; }

    public ItemConfig RilfeItemConfig { get => _rifleItemConfig; }
    public ItemConfig ShotgunItemConfig { get => _shotgunItemConfig; }

    public GunConfig RifleConfig { get => _rifleConfig; }
    public GunConfig ShotgunConfig { get => _shotgunConfig; }

    public GameObject HumanPrefab { get => _humanPrefab; }
    public GameObject MonsterPrefab { get => _monsterPrefab; }

    [Header("Core")]
    [SerializeField] private CameraConfig _camConfig;

    [Header("Items")]
    [SerializeField] private ItemConfig _rifleItemConfig;
    [SerializeField] private ItemConfig _shotgunItemConfig;

    [Header("Equipment")]
    [SerializeField] private GunConfig _rifleConfig;
    [SerializeField] private GunConfig _shotgunConfig;

    [Header("Characters")]
    [SerializeField] private GameObject _humanPrefab;
    [SerializeField] private GameObject _monsterPrefab;
}
