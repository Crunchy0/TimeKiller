using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(TargetPreservationSystem))]
public sealed class TargetPreservationSystem : UpdateSystem {
    AspectFactory<MobileAgentAspect> _agentFactory;
    Filter _targetAwareFilter;

    public override void OnAwake() {
        _agentFactory = World.GetAspectFactory<MobileAgentAspect>();
        _targetAwareFilter = World.Filter.
            Extend<MobileAgentAspect>().
            With<TargetObserverComponent>().
            Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach(Entity e in _targetAwareFilter)
        {
            var mobileAgent = _agentFactory.Get(e);
            ref var target = ref e.GetComponent<TargetObserverComponent>();

            target.isInSight = target.transform != null && IsInSight(target, mobileAgent.Actor.eye);

            float curTime = Time.time;
            if (target.isInSight)
            {
                target.lastTimeSeen = curTime;
            }
            else if (target.lastTimeSeen + target.forgetTime < curTime)
            {
                target.Forget();
            }
        }
    }

    private bool IsInSight(TargetObserverComponent target, Transform eye)
    {
        var bounds = target.transform.GetComponent<Collider>().bounds;
        Vector3 dir = (bounds.center - eye.position);
        if (dir.magnitude > target.sightDistance)
            return false;

        float angleHor = Mathf.Abs(Vector3.SignedAngle(eye.forward, dir, eye.up));
        float angleVer = Mathf.Abs(Vector3.SignedAngle(eye.forward, dir, eye.right));
        if (angleHor > target.fov.x / 2 || angleVer > target.fov.y / 2)
            return false;

        // TODO: Adjust to the collider's transform (up/down vectors)
        Vector3 bottom = bounds.center + (bounds.size.y / 2) * Vector3.down;
        Vector3 top = bottom + bounds.size.y * Vector3.up;
        bool isBottomVisible = true;
        bool isTopVisible = true;
        if (Physics.Raycast(eye.position, bottom - eye.position, out var hitBottom, target.sightDistance))
        {
            bool hitAgent = hitBottom.transform.gameObject.layer == LayerMask.NameToLayer("Population");
            if (!hitAgent)
                isBottomVisible = false;
        }
        if (Physics.Raycast(eye.position, top - eye.position, out var hitTop, target.sightDistance))
        {
            bool hitAgent = hitTop.transform.gameObject.layer == LayerMask.NameToLayer("Population");
            if (!hitAgent)
                isTopVisible = false;
        }

        if (isBottomVisible || isTopVisible)
            Debug.DrawLine(eye.position, eye.position + dir, Color.green);

        return isBottomVisible || isTopVisible;
    }
}