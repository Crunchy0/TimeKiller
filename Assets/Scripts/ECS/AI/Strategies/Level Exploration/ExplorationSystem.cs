using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class ExplorationSystem : CustomUpdateSystem
{
    AspectFactory<MobileAgentAspect> _agentFactory;
    Filter _explorers;

    public override void OnAwake() {
        _agentFactory = World.GetAspectFactory<MobileAgentAspect>();
        _explorers = World.Filter.
            Extend<MobileAgentAspect>().
            With<ExplorerComponent>().
            Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach (Entity e in _explorers)
        {
            var mobileAgent = _agentFactory.Get(e);

            var body = mobileAgent.Body;
            ref var agent = ref mobileAgent.Path;
            ref var actor = ref mobileAgent.Actor;
            ref var explorer = ref e.GetComponent<ExplorerComponent>();

            if (agent.pathNodeIdx < 0 || agent.pathNodeIdx >= agent.path.Length)
                actor.lookTarget = actor.eye.position + actor.eye.forward;
            else
                actor.lookTarget = agent.path[agent.pathNodeIdx];

            bool requiresTarget = explorer.targetZone == null;
            bool reachedTarget = (agent.destination - body.transform.position).magnitude < 1f;

            if (actor.currentZone == null || !requiresTarget && !reachedTarget)
                continue;

            float exploreRand = Random.Range(0f, 1f);
            bool stay = exploreRand > actor.config.ExplorationPenchant;
            explorer.targetZone = stay ? actor.currentZone : actor.currentZone.ChooseNextZone(actor.config.GroupId);
            agent.destination = explorer.targetZone.GetRandomPoint();
        }
    }
}