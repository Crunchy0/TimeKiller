using Scellecs.Morpeh;

public class Gun : IEquipment
{
    public void MainAction(World world, EntityId id)
    {
        var pullTriggerReq = world.GetRequest<PullGunTrigger>();
        pullTriggerReq.Publish(new PullGunTrigger { isPulled = true, entityId = id });
    }

    public void AlternativeAction(World world, EntityId id)
    {
        var pullTriggerReq = world.GetRequest<PullGunTrigger>();
        pullTriggerReq.Publish(new PullGunTrigger { isPulled = false, entityId = id });
    }
}
