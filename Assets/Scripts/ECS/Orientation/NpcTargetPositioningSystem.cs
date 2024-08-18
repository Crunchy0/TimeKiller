using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class NpcTargetPositioningSystem : CustomUpdateSystem {
    AspectFactory<AgentAspect> _agentsFactory;
    Stash<AgentPathComponent> _agentPathStash;
    Stash<TargetObserverComponent> _targetStash;
    Stash<AttackTargetComponent> _attackStash;
    Filter _agents;

    public override void OnAwake()
    {
        _agentsFactory = World.GetAspectFactory<AgentAspect>();
        _agentPathStash = World.GetStash<AgentPathComponent>();
        _targetStash = World.GetStash<TargetObserverComponent>();
        _attackStash = World.GetStash<AttackTargetComponent>();
        _agents = World.Filter.Extend<AgentAspect>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(Entity e in _agents)
        {
            var agent = _agentsFactory.Get(e);
            ref var actor = ref agent.Actor;

            // Look directly at the target
            if (_targetStash.Has(e) && _attackStash.Has(e))
            {
                var target = _targetStash.Get(e);
                if (target.IsAcquired)
                {
                    actor.lookTarget = target.transform.position;
                    actor.lookTarget.y = actor.eye.position.y;
                }
                continue;
            }

            // Look ahead like a dummie
            if(!_agentPathStash.Has(e))
            {
                actor.lookTarget = actor.eye.position + actor.eye.forward;
                continue;
            }

            // Look along the path
            var path = e.GetComponent<AgentPathComponent>();
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