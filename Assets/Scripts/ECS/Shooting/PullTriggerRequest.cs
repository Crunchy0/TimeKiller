using Scellecs.Morpeh;
public struct PullTriggerRequest : IRequestData
{
    public EntityId entityId;
    public bool isPulled;
}
