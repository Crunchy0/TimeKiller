using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Config", menuName = "ScriptableObject/Gun Config")]
public class GunConfig : ScriptableObject
{
    public int MaxAmmo { get => _maxAmmo; }
    public float TimeBetweenShots { get => _timeBetweenShots; }
    public bool IsAutoSupported { get => _isAutoSupported; }
    public int BulletsPerShot { get => _bulletsPerShot; }
    public float Spread { get => _spread; }

    [SerializeField] [Min(1)] private int _maxAmmo;
    [SerializeField] [Min(0.02f)] private float _timeBetweenShots;
    [SerializeField] private bool _isAutoSupported;
    [SerializeField] [Min(1)] private int _bulletsPerShot;
    [SerializeField] [Min(0)] private float _spread;
}
