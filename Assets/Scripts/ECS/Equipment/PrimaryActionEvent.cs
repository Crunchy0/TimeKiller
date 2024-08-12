using Scellecs.Morpeh;

public struct PrimaryActionEvent : IEventData
{
    public EntityId actorId;
    public bool activated;
}
