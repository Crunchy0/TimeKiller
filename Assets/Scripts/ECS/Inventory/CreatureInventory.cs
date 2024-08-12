using Scellecs.Morpeh;
using System.Collections.Generic;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct CreatureInventory : IComponent {
    public List<ItemEntry> inventory;
}