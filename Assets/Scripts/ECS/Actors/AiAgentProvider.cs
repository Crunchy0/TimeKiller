using Scellecs.Morpeh.Providers;
using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.AI;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class AiAgentProvider : MonoProvider<AiAgentComponent> {
    [SerializeField] [Min(0.01f)] float _pathUpdateTime;
    Timer _pathUpdateTimer;
    Stash<BodyComponent> _bodyStash;

    protected override void Initialize()
    {
        _bodyStash = World.Default.GetStash<BodyComponent>();
        _pathUpdateTimer = new Timer(_pathUpdateTime);
        _pathUpdateTimer.Reset();

        ref var agentComp = ref GetData();
        agentComp.path = new Vector3[0];
        agentComp.pathNodeIdx = -1;
        agentComp.pathUpdateTimer = _pathUpdateTimer;

        TimerManager.Subscribe(_pathUpdateTimer.Update);
        _pathUpdateTimer.TimerExpired += UpdatePath;
    }

    protected override void Deinitialize()
    {
        TimerManager.Unsubscribe(_pathUpdateTimer.Update);
        _pathUpdateTimer.TimerExpired -= UpdatePath;
    }

    private void UpdatePath()
    {
        _pathUpdateTimer.Reset();
        if (!_bodyStash.Has(Entity))
            return;

        var bodyComp = Entity.GetComponent<BodyComponent>();
        ref var agentComp = ref GetData();
        
        if ((agentComp.destination - bodyComp.transform.position).magnitude < 2f)
            return;

        NavMeshPath path = new();
        bool isPathExist = NavMesh.CalculatePath(bodyComp.transform.position, agentComp.destination, NavMesh.AllAreas, path);
        //bool isPathExist = agentComp.agent.CalculatePath(agentComp.destination, agentComp.path);
        agentComp.path = path.corners;
        agentComp.pathNodeIdx = isPathExist ? 0 : -1;
    }
}