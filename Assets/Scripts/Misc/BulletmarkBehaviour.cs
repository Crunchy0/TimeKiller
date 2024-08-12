using UnityEngine;

public class BulletmarkBehaviour : MonoBehaviour
{
    [SerializeField] [Min(0f)] float _vanishTime;

    void Start()
    {
        Destroy(gameObject, _vanishTime);
    }
}
