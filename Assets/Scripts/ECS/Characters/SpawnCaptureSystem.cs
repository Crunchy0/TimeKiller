using Scellecs.Morpeh;
using System;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class SpawnCaptureSystem : CustomUpdateSystem {
    Filter _spawners;

    public override void OnAwake()
    {
        _spawners = World.Filter.With<NpcSpawnerComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (Entity e in _spawners)
        {
            ref var spawner = ref e.GetComponent<NpcSpawnerComponent>();

            int ownerPop = spawner.zone.GetPopulation(spawner.ownerId);
            if (ownerPop > 0)
                continue;

            int maxPresent = 0;
            CharacterGroupId maxPresentId = CharacterGroupId.NONE;
            foreach (var groupId in Enum.GetValues(typeof(CharacterGroupId)))
            {
                int pop = spawner.zone.GetPopulation((CharacterGroupId)groupId);
                if (pop < 1)
                    continue;
                if (pop > maxPresent)
                {
                    maxPresent = pop;
                    maxPresentId = (CharacterGroupId)groupId;
                }
            }

            if (maxPresentId != CharacterGroupId.NONE)
                spawner.ownerId = maxPresentId;
        }
    }
}