using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPrototypeStorage
{
    readonly Dictionary<int, List<GameObject>> _prototypes = new();

    public NpcPrototypeStorage(ConfigBase cfgBase)
    {
        foreach (var proto in cfgBase.NpcPrefabs)
        {
            if (!proto.TryGetComponent<ActorProvider>(out var provider))
                continue;

            int id = (int)provider.GetData().config.GroupId;
            Add(id, proto);
        }
    }

    public bool GetGroup(CharacterGroupId id, out List<GameObject> group)
    {
        return _prototypes.TryGetValue((int)id, out group);
    }

    private void Add(int id, GameObject proto)
    {
        if (!_prototypes.ContainsKey(id))
            _prototypes.Add(id, new List<GameObject>());

        _prototypes[id].Add(proto);
    }
}
