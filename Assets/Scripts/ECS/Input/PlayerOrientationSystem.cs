using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class PlayerOrientationSystem : CustomUpdateSystem
{
    TargetInfo _targInfo;
    PlayerControlledEntity _controlled;

    public PlayerOrientationSystem(TargetInfo targInfo, PlayerControlledEntity controlled) =>
        (_targInfo, _controlled) = (targInfo, controlled);

    public override void OnAwake() {
    }

    public override void OnUpdate(float deltaTime) {
        Entity e;
        bool isOrientablePlayer = World.TryGetEntity(_controlled.Id, out e) &&
            e.Has<ActorComponent>();

        if (!isOrientablePlayer)
            return;

        ref var actorComp = ref e.GetComponent<ActorComponent>();
        actorComp.lookTarget = _targInfo.CrosshairTargPos;
    }

    [Inject]
    private void InjectDependencies(TargetInfo targInfo, PlayerControlledEntity servInfo)
    {
        _targInfo = targInfo;
        _controlled = servInfo;
    }
}