using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class TargetAcquirementSystem : CustomUpdateSystem
{
    AspectFactory<AgentAspect> _agentFactory;
    Filter _targetAwareFilter;
    GroupRelationMatrix _relMatrix;

    public TargetAcquirementSystem(GroupRelationMatrix relMatrix) => _relMatrix = relMatrix;

    public override void OnAwake()
    {
        _agentFactory = World.GetAspectFactory<AgentAspect>();
        _targetAwareFilter = World.Filter.
            Extend<AgentAspect>().
            With<TargetObserverComponent>().
            Without<AttackTargetComponent>().
            Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach(Entity e in _targetAwareFilter)
        {
            var mobileAgent = _agentFactory.Get(e);
            ref var target = ref e.GetComponent<TargetObserverComponent>();

            if(!target.IsAcquired)
            {
                TryAcquireTarget(mobileAgent, ref target);
            }
        }
    }

    private bool TryAcquireTarget(AgentAspect mobileAgent, ref TargetObserverComponent target)
    {
        var actor = mobileAgent.Actor;
        foreach (Collider col in Physics.OverlapSphere(actor.eye.position, target.sightDistance, LayerMask.GetMask("Population")))
        {
            var provider = col.transform.GetComponentInParent<EntityProvider>();
            if (provider == null || !provider.Entity.Has<ActorComponent>())
                continue;

            var otherActorComp = provider.Entity.GetComponent<ActorComponent>();
            var relation = _relMatrix.GetRelations(actor.config.GroupId)[(int)(otherActorComp.config.GroupId)];
            if (relation != GroupRelation.ENEMY)
                continue;

            target.id = provider.Entity.ID;
            target.transform = col.transform;  // To aim correctly, this is not enough :(
            return true;
        }
        return false;
    }
}