using Scellecs.Morpeh;

public struct SecondaryActionEvent : IEventData
{
    public EntityId actorId;
    public bool activated;
}
