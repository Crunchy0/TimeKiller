using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntry
{
    public ItemConfig ItemConfig { get => _config; }
    public IEntityState ItemState { get => _state; }

    private ItemConfig _config;
    private IEntityState _state;

    public ItemEntry(ItemConfig config, IEntityState state)
    {
        _config = config;
        _state = state;
    }
}
