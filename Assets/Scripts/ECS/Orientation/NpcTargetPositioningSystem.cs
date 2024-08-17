using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class NpcTargetPositioningSystem : CustomUpdateSystem {
    AspectFactory<MobileAgentAspect> _agentsFactory;
    Stash<TargetObserverComponent> _targetStash;
    Stash<AttackTargetComponent> _attackStash;
    Filter _agents;

    public override void OnAwake()
    {
        _agentsFactory = World.GetAspectFactory<MobileAgentAspect>();
        _targetStash = World.GetStash<TargetObserverComponent>();
        _attackStash = World.GetStash<AttackTargetComponent>();
        _agents = World.Filter.Extend<MobileAgentAspect>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(Entity e in _agents)
        {
            var agent = _agentsFactory.Get(e);
            ref var actor = ref agent.Actor;

            if (_targetStash.Has(e) && _attackStash.Has(e))
            {
                var target = _targetStash.Get(e);
                actor.lookTarget = target.transform.position;
                actor.lookTarget.y = actor.eye.position.y;
                continue;
            }

            var path = agent.Path;
            int length = path.path.Length;
            if (length > 0 && path.pathNodeIdx < length)
            {
                actor.lookTarget = path.path[path.pathNodeIdx];
                actor.lookTarget.y = actor.eye.position.y;
            }
            else if(path.destination != null)
            {
                actor.lookTarget = path.destination;
                actor.lookTarget.y = actor.eye.position.y;
            }
        }
    }
}