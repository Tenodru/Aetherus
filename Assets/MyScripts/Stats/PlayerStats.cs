using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player stat class.
/// </summary>
public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Updates player stats when items are equipped or unequipped.
    /// </summary>
    /// <param name="newItem"></param>
    /// <param name="oldItem"></param>
    void OnEquipmentChanged (Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }
        if (oldItem != null)
        {
            armor.RemoveModifier(newItem.armorModifier);
            damage.RemoveModifier(newItem.damageModifier);
        }

    }

    /// <summary>
    /// Called when player reaches 0 health or below.
    /// </summary>
    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }
}
