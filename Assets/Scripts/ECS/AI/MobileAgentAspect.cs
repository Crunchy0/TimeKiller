using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct MobileAgentAspect : IAspect, IFilterExtension
{
    public Entity Entity { get; set; }

    public ref BodyComponent Body => ref _bodyStash.Get(Entity);
    public ref MovementComponent Movement => ref _movementStash.Get(Entity);
    public ref ActorComponent Actor => ref _actorStash.Get(Entity);
    public ref AgentPathComponent Path => ref _pathStash.Get(Entity);

    Stash<BodyComponent> _bodyStash;
    Stash<MovementComponent> _movementStash;
    Stash<ActorComponent> _actorStash;
    Stash<AgentPathComponent> _pathStash;

    public void OnGetAspectFactory(World world)
    {
        _bodyStash = world.GetStash<BodyComponent>();
        _movementStash = world.GetStash<MovementComponent>();
        _actorStash = world.GetStash<ActorComponent>();
        _pathStash = world.GetStash<AgentPathComponent>();
    }

    public FilterBuilder Extend(FilterBuilder builder)
    {
        return builder.
            With<BodyComponent>().
            With<MovementComponent>().
            With<ActorComponent>().
            With<AgentPathComponent>().
            Without<PlayerComponent>();
    }
}