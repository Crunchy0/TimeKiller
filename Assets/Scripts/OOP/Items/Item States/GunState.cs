using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunState : IEntityState
{
    public int AmmoLeft
    {
        get => _ammoLeft;
        set => _ammoLeft = value > 0 ? value : 0;
    }

    public bool IsAutoEnabled
    {
        get => _isAutoEnabled;
        set => _isAutoEnabled = _gunConfig.IsAutoSupported ? value : false;
    }

    public Timer ShotTimer { get => _shotTimer; }

    public bool IsTriggerPulled { get; set; } = false;
    public bool ShotOnce { get; set; } = false;

    private int _ammoLeft;
    private bool _isAutoEnabled;
    private Timer _shotTimer;
    private GunConfig _gunConfig;

    public GunState(GunConfig gunConfig)
    {
        _gunConfig = gunConfig;
        AmmoLeft = _gunConfig.MaxAmmo;
        _isAutoEnabled = _gunConfig.IsAutoSupported;

        _shotTimer = new Timer();
        _shotTimer.Duration = _gunConfig.TimeBetweenShots;
        TimerManager.Subscribe(_shotTimer.Update);
    }

    ~GunState()
    {
        TimerManager.Unsubscribe(_shotTimer.Update);
    }

    public void ResetTimer()
    {
        _shotTimer.Duration = _gunConfig.TimeBetweenShots;
        _shotTimer.Reset();
    }

    public string TakeSnapshot()
    {
        return JsonUtility.ToJson(_shotTimer);
    }

    public void LoadSnapshot(string snapshot)
    {
        JsonUtility.FromJsonOverwrite(snapshot, this);
    }
}
