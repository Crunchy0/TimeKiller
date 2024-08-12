using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scellecs.Morpeh;

public struct SpawnBulletRequest : IRequestData
{
    public EntityId targetEntityId;
}
