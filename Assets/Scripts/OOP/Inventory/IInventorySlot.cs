public interface IInventorySlot
{
    public int Amount { get; set; }
    public ItemEntry Item { get; }
    public bool IsEmpty { get; }
    public void RemoveItem(out ItemEntry item, out int amount);
    public bool SetItem(ItemEntry item);
}
