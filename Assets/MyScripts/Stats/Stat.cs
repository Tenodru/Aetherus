using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Outline for Stat classes.
/// </summary>
[System.Serializable]
public class Stat 
{
    [SerializeField] private int baseValue;

    private List<int> modifiers = new List<int>();

    /// <summary>
    /// Returns base value of stat.
    /// </summary>
    /// <returns></returns>
    public int GetValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    /// <summary>
    /// Adds int modifier to List of stat modifiers.
    /// </summary>
    /// <param name="modifier"></param>
    public void AddModifier (int modifier)
    {
        if (modifier != 0)
            modifiers.Add(modifier);
    }

    public void RemoveModifier (int modifier)
    {
        if (modifier != 0)
            modifiers.Remove(modifier);
    }
}
