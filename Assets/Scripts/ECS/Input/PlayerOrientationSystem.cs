using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerOrientationSystem))]
public sealed class PlayerOrientationSystem : UpdateSystem {
    TargetInfo _targInfo;
    PlayerControlledEntity _servInfo;

    public override void OnAwake() {
    }

    public override void OnUpdate(float deltaTime) {
        Entity e;
        bool isOrientablePlayer = World.TryGetEntity(_servInfo.ControlledId, out e) &&
            e.Has<ActorComponent>();

        if (!isOrientablePlayer)
            return;

        ref var actorComp = ref e.GetComponent<ActorComponent>();
        actorComp.target = _targInfo.CrosshairTargPos;
    }

    [Inject]
    private void InjectDependencies(TargetInfo targInfo, PlayerControlledEntity servInfo)
    {
        _targInfo = targInfo;
        _servInfo = servInfo;
    }
}