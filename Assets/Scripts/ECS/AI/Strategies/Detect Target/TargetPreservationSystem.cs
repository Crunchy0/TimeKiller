using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class TargetPreservationSystem : CustomUpdateSystem
{
    AspectFactory<AgentAspect> _agentFactory;
    Filter _targetAwareFilter;

    public override void OnAwake() {
        _agentFactory = World.GetAspectFactory<AgentAspect>();
        _targetAwareFilter = World.Filter.
            Extend<AgentAspect>().
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

        bool rcast = Physics.Raycast(eye.position, bottom - eye.position, out var hitBottom, target.sightDistance);
        if (rcast && HitId(hitBottom) == target.id)
        {
            Debug.DrawLine(eye.position, hitBottom.transform.position, Color.green);
            return true;
        }

        rcast = Physics.Raycast(eye.position, top - eye.position, out var hitTop, target.sightDistance);
        if (rcast && HitId(hitTop) == target.id)
        {
            Debug.DrawLine(eye.position, hitTop.transform.position, Color.green);
            return true;
        }

        return false;
    }

    private EntityId HitId(RaycastHit hit)
    {
        bool hitAgent = hit.transform.gameObject.layer == LayerMask.NameToLayer("Population");
        var provider = hit.transform.GetComponentInParent<EntityProvider>();
        bool invalid = !hitAgent || provider == null || provider.Entity.IsNullOrDisposed();
        return invalid ? EntityId.Invalid : provider.Entity.ID;
    }
}