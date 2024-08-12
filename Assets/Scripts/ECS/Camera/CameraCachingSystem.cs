using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(CameraCachingSystem))]
public sealed class CameraCachingSystem : UpdateSystem {
    CameraController _camController;
    float _cachingInterval;
    float _lastCachingTime;

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

    [Inject]
    private void InjectDependencies(CameraController camController)
    {
        _camController = camController;
    }
}