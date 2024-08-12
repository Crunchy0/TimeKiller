using Scellecs.Morpeh;

public class Gun : IEquipment
{
    public void MainAction(World world, EntityId id)
    {
        var pullTriggerReq = world.GetRequest<PullTriggerRequest>();
        pullTriggerReq.Publish(new PullTriggerRequest { isPulled = true, entityId = id });
    }

    public void AlternativeAction(World world, EntityId id)
    {
        var pullTriggerReq = world.GetRequest<PullTriggerRequest>();
        pullTriggerReq.Publish(new PullTriggerRequest { isPulled = false, entityId = id });
    }
}
