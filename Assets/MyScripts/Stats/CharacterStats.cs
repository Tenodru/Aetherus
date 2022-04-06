using UnityEngine;

/// <summary>
/// Character stat class.
/// </summary>
public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Applies damage to transform.
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage (int damage)
    {
        damage -= armor.GetValue();                                     //Decreases damage value based on armor value.
        damage = Mathf.Clamp(damage, 0, int.MaxValue);                  //Clamps damage so it can be reduced up to 0 by armor.
        currentHealth -= damage;                                        //Reduces health by final damage number.
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Death method.
    /// </summary>
    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
    }
}
