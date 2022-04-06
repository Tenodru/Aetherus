using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Enemy health management utility.
/// </summary>
public class Health : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;

    /// <summary>
    /// Gets this creature's current health.
    /// </summary>
    /// <returns></returns>
    public int GetHealth()
    {
        return health;
    }

    /// <summary>
    /// Gets this creature's starting health.
    /// </summary>
    /// <returns></returns>
    public int GetStartingHealth()
    {
        return startingHealth;
    }

    /// <summary>
    /// Changes this creature's health.
    /// </summary>
    /// <param name="add"></param>
    public void ChangeHealth(int change)
    {
        health += change;
        Debug.Log("Changed health.");
    }
}
