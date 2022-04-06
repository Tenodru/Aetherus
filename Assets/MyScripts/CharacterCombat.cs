using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    public float attackDelay = 0.5f;

    public event System.Action OnAttack;

    CharacterStats myStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;       //Every frame, reduce attackCooldown
    }

    /// <summary>
    /// Attacks target.
    /// </summary>
    /// <param name="targetStats"></param>
    public void Attack (CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));
            OnAttack?.Invoke();                                     //Invokes OnAttack.
            attackCooldown = 1f / attackSpeed;                      //The higher the attack speed, the shorter the attack CD
        }
    }

    /// <summary>
    /// Casts damage dealt after a delay. Used with attack animations.
    /// </summary>
    /// <param name="stats"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    IEnumerator DoDamage (CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.damage.GetValue());
    }
}
