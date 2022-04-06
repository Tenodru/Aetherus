using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    /// <summary>
    /// Called when this enemy dies.
    /// </summary>
    public override void Die()
    {
        base.Die();

        Destroy(gameObject);
    }
}
