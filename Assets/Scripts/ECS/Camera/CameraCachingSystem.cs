using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class CameraCachingSystem : CustomUpdateSystem {
    CameraController _camController;
    float _cachingInterval;
    float _lastCachingTime;

    public CameraCachingSystem(CameraController camController) => _camController = camController;

    public override void OnAwake() {
        _cachingInterval = 1;
        _lastCachingTime = Time.time;
    }

    public override void OnUpdate(float deltaTime) {
        float currentTime = Time.time;
        if (currentTime - _lastCachingTime < _cachingInterval)
            return;

        _camController.CacheCurrentState();
        _lastCachingTime = currentTime;
    }
}