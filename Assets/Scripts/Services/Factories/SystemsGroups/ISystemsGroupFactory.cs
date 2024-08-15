using Scellecs.Morpeh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISystemsGroupFactory
{
    public SystemsGroup Create(World world);
}
