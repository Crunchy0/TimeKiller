using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigBase : MonoBehaviour
{
    public CameraConfig CamConfig { get => _camConfig; }

    public GunConfig RifleConfig { get => _rifleConfig; }
    public GunConfig ShotgunConfig { get => _shotgunConfig; }

    public List<GameObject> NpcPrefabs { get => _npcPrefabs; }

    [Header("Core")]
    [SerializeField] private CameraConfig _camConfig;

    [Header("Equipment")]
    [SerializeField] private GunConfig _rifleConfig;
    [SerializeField] private GunConfig _shotgunConfig;

    [Header("Characters")]
    [SerializeField] private List<GameObject> _npcPrefabs;
}
