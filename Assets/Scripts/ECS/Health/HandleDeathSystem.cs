using Scellecs.Morpeh;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class HandleDeathSystem : CustomUpdateSystem {
    AspectFactory<AgentAspect> _agentFactory;
    Filter _living;
    List<GameObject> _corpses = new();

    public override void OnAwake()
    {
        _agentFactory = World.GetAspectFactory<AgentAspect>();
        _living = World.Filter.Extend<AgentAspect>().With<HealthComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        // This is just a stub - in normal games you would enable ragdoll and all that
        // TODO: more realistic death
        foreach (Entity e in _living)
        {
            var agent = _agentFactory.Get(e);
            var health = e.GetComponent<HealthComponent>();
            if (health.hp <= 1e-3)
            {
                if (agent.Actor.currentZone != null)
                    agent.Actor.currentZone.DecreasePopulation(agent.Actor.config.GroupId);
                e.RemoveComponent<HealthComponent>();
                _corpses.Add(agent.Body.transform.gameObject);
                World.RemoveEntity(e);
            }
        }
        while (_corpses.Count > 0)
        {
            GameObject.Destroy(_corpses[0]);
            _corpses.RemoveAt(0);
        }
    }
}