using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class AnimatedAttackDisableSystem : CustomUpdateSystem
{
    Request<AnimatedAttackStopRequest> _stopAttackReq;

    public override void OnAwake()
    {
        _stopAttackReq = World.GetRequest<AnimatedAttackStopRequest>();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var req in _stopAttackReq.Consume())
        {
            if (!World.TryGetEntity(req.actorId, out Entity e) || e.IsNullOrDisposed())
                continue;

            ref var attack = ref e.GetComponent<AnimationBasedAttackerComponent>();
            attack.stopTime = Time.time;
        }
    }
}