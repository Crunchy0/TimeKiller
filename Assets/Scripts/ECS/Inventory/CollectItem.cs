using Scellecs.Morpeh;
using UnityEngine;

public struct CollectItem : IRequestData
{
    public EntityId itemId;
    public EntityId collectorId;
    public GameObject itemObject;
}
