using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using VContainer;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(TargetAcquirementSystem))]
public sealed class TargetAcquirementSystem : UpdateSystem {
    AspectFactory<MobileAgentAspect> _agentFactory;
    Filter _targetAwareFilter;
    GroupRelationMatrix _relMatrix;

    public override void OnAwake()
    {
        _agentFactory = World.GetAspectFactory<MobileAgentAspect>();
        _targetAwareFilter = World.Filter.
            Extend<MobileAgentAspect>().
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

    private bool TryAcquireTarget(MobileAgentAspect mobileAgent, ref TargetObserverComponent target)
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

    [Inject]
    private void InjectDependencies(GroupRelationMatrix relMatrix)
    {
        _relMatrix = relMatrix;
    }
}