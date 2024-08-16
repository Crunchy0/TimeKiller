using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class AiNavigationSystem : CustomUpdateSystem
{
    AspectFactory<MobileAgentAspect> _agentFactory;
    Filter _aiMovementFilter;
    GroupRelationMatrix _relMatrix;

    public AiNavigationSystem(GroupRelationMatrix relMatrix) => _relMatrix = relMatrix;

    public override void OnAwake()
    {
        _agentFactory = World.GetAspectFactory<MobileAgentAspect>();
        _aiMovementFilter = World.Filter.
            With<ActorComponent>().
            With<AgentPathComponent>().
            With<BodyComponent>().
            With<MovementComponent>().
            Without<PlayerComponent>().
            Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (Entity e in _aiMovementFilter)
        {
            var mobileAgent = _agentFactory.Get(e);
            ref var body = ref mobileAgent.Body;
            ref var movement = ref mobileAgent.Movement;
            ref var agent = ref mobileAgent.Path;
            ref var actor = ref mobileAgent.Actor;

            int i = agent.pathNodeIdx;
            int pathLen = agent.path.Length;
            if (i < 0 || i >= pathLen)
                continue;

            Vector3 moveTo = agent.path[i];
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
                if (!IsObstacleEnemy(actor.config.GroupId, gameObject))
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
            for(int k = 0; k < agent.path.Length - 1; k++)
            {
                Debug.DrawLine(agent.path[k], agent.path[k + 1], Color.red);
            }

            if ((moveTo - body.transform.position).magnitude < 1f)
            {
                movement.direction = Vector2.zero;
                agent.pathNodeIdx++;
                //if (agent.pathNodeIdx < agent.path.Length)
                //    actor.target = agent.path[agent.pathNodeIdx];
            }
        }
    }

    private bool IsObstacleEnemy(CharacterGroupId charId, GameObject gameObject)
    {
        var provider = gameObject.GetComponentInParent<AgentPathProvider>();
        if (provider == null || !provider.Entity.Has<ActorComponent>())
            return false;

        var actor = provider.Entity.GetComponent<ActorComponent>();
        bool isEnemy = _relMatrix.GetRelations(charId)[(int)actor.config.GroupId] == GroupRelation.ENEMY;
        return isEnemy;
    }
}