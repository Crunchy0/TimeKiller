using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;

public class PlayerControlledEntity
{
    public EntityId ControlledId { get; private set; }
    public GameObject Controlled { get; private set; }

    private EntityId _characterPickerId;
    private GameObject _characterPicker;

    public PlayerControlledEntity(GameObject picker)
    {
        _characterPicker = picker;
        _characterPickerId = _characterPicker.GetComponent<EntityProvider>().Entity.ID;

        Controlled = _characterPicker;
        ControlledId = _characterPickerId;
    }

    public void TakeControl(GameObject gameObject = null)
    {
        EntityProvider provider = null;
        bool defaultServant = gameObject == null || !gameObject.TryGetComponent(out provider);

        if (defaultServant)
        {
            _characterPicker.transform.position = Controlled.transform.position;
            if (World.Default.TryGetEntity(ControlledId, out Entity e) && e.Has<PlayerComponent>())
            {
                e.RemoveComponent<PlayerComponent>();
            }
        }
        else
        {
            provider.Entity.AddComponent<PlayerComponent>();
        }

        Controlled = defaultServant ? _characterPicker : gameObject;
        ControlledId = defaultServant ? _characterPickerId : provider.Entity.ID;
    }
}
