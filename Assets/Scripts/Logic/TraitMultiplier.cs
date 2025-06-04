using System.Collections.Generic;
using UnityEngine;

public class TraitMultiplier
{
    public Dictionary<string, float> multipliers = new();

    public void SetMultiplier(string trait, float value)
    {
        if (!multipliers.ContainsKey(trait))
            multipliers[trait] = value;
    }

    public float GetMultiplier(string trait)
    {
        return multipliers.ContainsKey(trait) ? multipliers[trait] : 1f;
    }

    public void Clear()
    {
        multipliers.Clear();
    }
}
