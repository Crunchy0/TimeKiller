using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public List<Zone> AdjacentZones { get => _adjacentZones; }
    public int OverallPopulation { get; private set; } = 0;

    [SerializeField] List<Zone> _adjacentZones;
    protected Dictionary<CharacterGroupId, int> _population = new();

    public Vector3 GetRandomPoint()
    {
        float x, z;

        var collider = GetComponent<Collider>();
        float boundX = collider.bounds.size.x;
        float boundZ = collider.bounds.size.z;

        float minX = transform.position.x - boundX / 2;
        float minZ = transform.position.z - boundZ / 2;

        x = Random.Range(minX, minX + boundX);
        z = Random.Range(minZ, minZ + boundZ);

        return new(x, transform.position.y, z);
    }

    public Zone ChooseNextZone(CharacterGroupId groupId)
    {
        if (_adjacentZones.Count < 1)
            return this;
        // In case the zone population is 0, this value is used in calculations (maybe setup in config?)
        // If zone population is P, then its preference is 1/P, so this value should be greater than 1
        float emptyZonePreference = 1.5f;

        float normingSum = 0f;
        int[] pops = new int[_adjacentZones.Count];
        int overallPop = 0;
        float[] preferences = new float[_adjacentZones.Count];
        for (int j = 0; j < _adjacentZones.Count; j++)
        {
            int pop = _adjacentZones[j].GetPopulation(groupId);
            pops[j] = pop;
            overallPop += pop;
            preferences[j] = pop > 0 ? (1f / pop) : emptyZonePreference;
            normingSum += preferences[j];
        }

        float[] cdf = new float[_adjacentZones.Count];
        cdf[0] = preferences[0] / normingSum;
        float sum = cdf[0];
        for (int j = 1; j < _adjacentZones.Count; j++)
        {
            cdf[j] = cdf[j - 1] + preferences[j] / normingSum;
            sum += preferences[j] / normingSum;
        }

        float zoneRand = Random.Range(0f, 1f);
        int i = 0;
        while (cdf[i] < zoneRand)
            i++;
        return _adjacentZones[i];
    }

    public int GetPopulation(CharacterGroupId key)
    {
        if (_population.TryGetValue(key, out int value))
            return value;
        return 0;
    }

    public void IncreasePopulation(CharacterGroupId key)
    {
        if (_population.ContainsKey(key))
            _population[key] += 1;
        else
            _population.Add(key, 1);

        OverallPopulation += 1;
    }

    public virtual void DecreasePopulation(CharacterGroupId key)
    {
        if (!_population.ContainsKey(key) || _population[key] < 1)
            return;

        _population[key] -= 1;
        OverallPopulation -= 1;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    foreach(var zone in _adjacentZones)
    //    {
    //        Gizmos.DrawLine(transform.position, zone.transform.position);
    //    }
    //}
}
