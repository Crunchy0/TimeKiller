using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;

public class PlayerControlledEntity
{
    public EntityId Id { get; private set; }
    public GameObject Object { get; private set; }

    private EntityId _defaultId;
    private GameObject _defaultObject;

    public PlayerControlledEntity(GameObject defaultObject)
    {
        _defaultObject = defaultObject;
        _defaultId = _defaultObject.GetComponent<EntityProvider>().Entity.ID;

        Object = _defaultObject;
        Id = _defaultId;
    }

    public void TakeControl(GameObject gameObject = null)
    {
        EntityProvider provider = null;
        bool def = gameObject == null || !gameObject.TryGetComponent(out provider);

        if (def)
        {
            _defaultObject.transform.position = Object.transform.position;
            if (World.Default.TryGetEntity(Id, out Entity e) && e.Has<PlayerComponent>())
            {
                e.RemoveComponent<PlayerComponent>();
            }
        }
        else
        {
            provider.Entity.AddComponent<PlayerComponent>();
        }

        Object = def ? _defaultObject : gameObject;
        Id = def ? _defaultId : provider.Entity.ID;
    }
}
