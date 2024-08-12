using Scellecs.Morpeh.Providers;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using TriInspector;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class CharacterPickerProvider : EntityProvider {
    [SerializeField] [Required] Rigidbody _rigidbody;

    protected override void Initialize()
    {
        ref var bodyComp = ref Entity.AddComponent<BodyComponent>();
        bodyComp.transform = transform;
        bodyComp.rigidbody = _rigidbody;

        ref var eqComp = ref Entity.AddComponent<Equipment>();
        eqComp.main = (World w) => { };
        eqComp.mainStop = SendPickEvent;
        eqComp.alt = (World w) => { };
        eqComp.altStop = (World w) => { };

        ref var activeEqComp = ref Entity.AddComponent<ActiveEquipment>();
        activeEqComp.equippedId = Entity.ID;
        activeEqComp.gameObject = gameObject;
        activeEqComp.inventoryIdx = -1;
    }

    private void SendPickEvent(World world)
    {
        var evt = world.GetEvent<PickCharacterEvent>();
        evt.NextFrame(new PickCharacterEvent { });
    }
}