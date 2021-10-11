using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Weighted : MonoBehaviour
{
    public float Weight;
}

public static class WeightedExtensions
{
    public static float GetWeight(this MonoBehaviour component)
    {
        if (component.HasComponent<Weighted>())
            return component.GetComponent<Weighted>().Weight;

        return 1;
    }

    public static T ChooseComponentAtRandom<T>(this IEnumerable<T> components) 
        where T : MonoBehaviour
    {
        return MathUtility.ChooseAtRandom(
            components,
            component => component.GetWeight());
    }

    public static float AverageComponentValue<T>(this IEnumerable<T> components, Func<T, float> GetValue)
        where T : MonoBehaviour
    {
        return components.WeightedAverage(
            GetValue, 
            component => component.GetWeight());
    }
}

