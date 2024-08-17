using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class ExplorationSystem : CustomUpdateSystem
{
    AspectFactory<AgentAspect> _agentFactory;
    Filter _explorers;

    public override void OnAwake() {
        _agentFactory = World.GetAspectFactory<AgentAspect>();
        _explorers = World.Filter.
            Extend<AgentAspect>().
            With<AgentPathComponent>().
            With<ExplorerComponent>().
            Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach (Entity e in _explorers)
        {
            var mobileAgent = _agentFactory.Get(e);

            var body = mobileAgent.Body;
            ref var actor = ref mobileAgent.Actor;
            ref var agentPath = ref e.GetComponent<AgentPathComponent>();
            ref var explorer = ref e.GetComponent<ExplorerComponent>();

            bool requiresTarget = explorer.targetZone == null;
            bool reachedTarget = (agentPath.destination - body.transform.position).magnitude < 1f;

            if (actor.currentZone == null || !requiresTarget && !reachedTarget)
                continue;

            float exploreRand = Random.Range(0f, 1f);
            bool stay = exploreRand > actor.config.ExplorationPenchant;
            explorer.targetZone = stay ? actor.currentZone : actor.currentZone.ChooseNextZone(actor.config.GroupId);
            agentPath.destination = explorer.targetZone.GetRandomPoint();
        }
    }
}