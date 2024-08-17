using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct AgentAspect : IAspect, IFilterExtension
{
    public Entity Entity { get; set; }

    public ref BodyComponent Body => ref _bodyStash.Get(Entity);
    public ref ActorComponent Actor => ref _actorStash.Get(Entity);

    Stash<BodyComponent> _bodyStash;
    Stash<ActorComponent> _actorStash;

    public void OnGetAspectFactory(World world)
    {
        _bodyStash = world.GetStash<BodyComponent>();
        _actorStash = world.GetStash<ActorComponent>();
    }

    public FilterBuilder Extend(FilterBuilder builder)
    {
        return builder.
            With<BodyComponent>().
            With<ActorComponent>().
            Without<PlayerComponent>();
    }
}