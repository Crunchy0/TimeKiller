using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class AiNavigationSystem : CustomUpdateSystem
{
    AspectFactory<AgentAspect> _agentFactory;
    Filter _aiMovementFilter;
    GroupRelationMatrix _relMatrix;

    public AiNavigationSystem(GroupRelationMatrix relMatrix) => _relMatrix = relMatrix;

    public override void OnAwake()
    {
        _agentFactory = World.GetAspectFactory<AgentAspect>();
        _aiMovementFilter = World.Filter.
            Extend<AgentAspect>().
            With<AgentPathComponent>().
            With<MovementComponent>().
            Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (Entity e in _aiMovementFilter)
        {
            var agent = _agentFactory.Get(e);
            ref var body = ref agent.Body;
            ref var actor = ref agent.Actor;
            ref var movement = ref e.GetComponent<MovementComponent>();
            ref var agentPath = ref e.GetComponent<AgentPathComponent>();

            int i = agentPath.pathNodeIdx;
            int pathLen = agentPath.path.Length;
            if (i < 0 || i >= pathLen)
                continue;

            Vector3 moveTo = agentPath.path[i];
            Vector3 dir = moveTo - body.transform.position;
            Vector3 velocity = body.rigidbody.velocity;
            dir.y = 0;

            Vector3 offset = dir - velocity;
            Vector3 resultDir;
            if (offset.magnitude < 5f)
                resultDir = dir.magnitude < 1 ? dir : dir.normalized;
            else
                resultDir = offset.normalized;

            if (Physics.Raycast(actor.eye.position, actor.eye.forward, out var hit, 5f))
            {
                var gameObject = hit.collider.gameObject;
                if (IsObstacleNeutral(actor.config.GroupId, gameObject))
                {
                    resultDir += 2f * actor.eye.right;
                    resultDir.Normalize();
                }
            }

            movement.direction.x = resultDir.x;
            movement.direction.y = resultDir.z;

            //Debug.DrawLine(body.transform.position, body.transform.position + 3*resultDir, Color.white);
            //Debug.DrawLine(body.transform.position, moveTo, Color.cyan);
            //Debug.DrawLine(body.transform.position, body.transform.position + body.rigidbody.velocity, Color.yellow);
            for(int k = 0; k < agentPath.path.Length - 1; k++)
            {
                Debug.DrawLine(agentPath.path[k], agentPath.path[k + 1], Color.red);
            }

            if ((moveTo - body.transform.position).magnitude < 1f)
            {
                movement.direction = Vector2.zero;
                agentPath.pathNodeIdx++;
                //if (agent.pathNodeIdx < agent.path.Length)
                //    actor.target = agent.path[agent.pathNodeIdx];
            }
        }
    }

    private bool IsObstacleNeutral(CharacterGroupId charId, GameObject gameObject)
    {
        var provider = gameObject.GetComponentInParent<AgentPathProvider>();
        if (provider == null || provider.Entity.IsNullOrDisposed() || !provider.Entity.Has<ActorComponent>())
            return false;

        var actor = provider.Entity.GetComponent<ActorComponent>();
        bool isNeutral = _relMatrix.GetRelations(charId)[(int)actor.config.GroupId] == GroupRelation.NEUTRAL;
        return isNeutral;
    }
}