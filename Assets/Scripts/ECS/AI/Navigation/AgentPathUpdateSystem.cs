using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.AI;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class AgentPathUpdateSystem : CustomUpdateSystem {
    AspectFactory<MobileAgentAspect> _agentFactory;
    Filter _agents;

    public override void OnAwake()
    {
        _agentFactory = World.GetAspectFactory<MobileAgentAspect>();
        _agents = World.Filter.Extend<MobileAgentAspect>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach(Entity e in _agents)
        {
            var mobileAgent = _agentFactory.Get(e);
            var body = mobileAgent.Body;
            ref var agentPath = ref mobileAgent.Path;

            float curTime = Time.time;
            if (agentPath.lastUpdatePath + agentPath.pathUpdateInterval > curTime)
                continue;

            agentPath.lastUpdatePath = curTime;
            if ((agentPath.destination - body.transform.position).magnitude < agentPath.destinationProximity)
            {
                agentPath.path = new Vector3[0];
                agentPath.pathNodeIdx = -1;
                continue;
            }

            NavMeshPath navPath = new();
            bool isPathExist = NavMesh.CalculatePath(body.transform.position, agentPath.destination, NavMesh.AllAreas, navPath);
            agentPath.path = navPath.corners;
            agentPath.pathNodeIdx = isPathExist ? 0 : -1;
        }
    }
}