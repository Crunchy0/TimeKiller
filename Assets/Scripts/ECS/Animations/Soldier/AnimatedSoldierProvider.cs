using TriInspector;
using UnityEngine;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class AnimatedSoldierProvider : MonoProvider<AnimatedCharacterComponent> {
    [Required] [SerializeField] Animator _animator;
    [SerializeField] AnimationConfig _config;

    protected override void Initialize()
    {
        ref var animated = ref GetData();
        animated.controller = new WalkAnimationController(_config, _animator);
    }
}