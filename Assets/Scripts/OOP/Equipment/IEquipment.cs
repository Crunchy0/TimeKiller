using Scellecs.Morpeh;

public interface IEquipment
{
    public void MainAction(World world, EntityId id);
    public void AlternativeAction(World world, EntityId id);
}
