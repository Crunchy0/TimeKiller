public class InventorySlot : IInventorySlot
{
    public int Amount { get => _amount; set => _amount = value >= 0 ? value : _amount; }
    public ItemEntry Item { get => _item; }
    public bool IsEmpty { get => _item == null; }

    private ItemEntry _item = null;
    private int _amount = 0;
    
    public InventorySlot(ItemEntry item = null)
    {
        SetItem(item);
    }

    public void RemoveItem(out ItemEntry item, out int amount)
    {
        item = Item;
        amount = Amount;

        SetItem(null);
        _amount = 0;
    }
    public bool SetItem(ItemEntry item)
    {
        if (_item == item)
            return false;

        _item = item;
        return true;
    }
}
