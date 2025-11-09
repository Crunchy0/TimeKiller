using System.Collections.Generic;
using UnityEngine;

public static class ListNormalizerExtensions
{
    public static float EuclideanNorm(this List<float> orig)
    {
        float squareSum = 0f;
        foreach (float component in orig)
            squareSum += component * component;
        return Mathf.Sqrt(squareSum);
    }

    public static void EuqlideanNormalize(this List<float> orig, float precision)
    {
        float norm = EuclideanNorm(orig);
        if (Mathf.Abs(1f - norm) < precision)
            return;

        for(int i = 0; i < orig.Count; i++)
            orig[i] /= norm;
    }
}
