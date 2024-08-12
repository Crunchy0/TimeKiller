using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

public delegate void EquipmentAction(World world);

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct Equipment : IComponent {
    public EquipmentAction main;
    public EquipmentAction mainStop;
    public EquipmentAction alt;
    public EquipmentAction altStop;
}