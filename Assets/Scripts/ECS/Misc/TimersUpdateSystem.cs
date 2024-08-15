using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class TimersUpdateSystem : CustomUpdateSystem {
    public override void OnAwake() {
    }

    public override void OnUpdate(float deltaTime) {
        TimerManager.UpdateAllTimers(deltaTime);
    }
}