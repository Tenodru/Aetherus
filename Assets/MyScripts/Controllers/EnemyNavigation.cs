using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
public class EnemyNavigation : MonoBehaviour
{
    Transform crystal;
    Transform player;

    public int enemyType = 0;
    public float lookRadius = 15f;
    public float attackRadius = 10f;
    private bool hasTarget = false;

    Animator anim;
    NavMeshAgent navMeshAgent;
    CharacterCombat combat;

    public float enemySpeed;

    // Start is called before the first frame update
    void Start()
    {
        enemySpeed = 2.5f;
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = 10;
        combat = GetComponent<CharacterCombat>();

        crystal = CrystalManager.instance.crystal.transform;
        player = PlayerManager.instance.player.transform;

        _SetDestination();

    }

    // Update is called once per frame
    void Update()
    {
        _SetDestination();
        Detect();
    }

    /// <summary>
    /// Sets default destination and speed.
    /// </summary>
    private void _SetDestination()
    {
        if (!hasTarget)
        {
            Animate(1);
            Vector3 targetVector = crystal.position;
            navMeshAgent.SetDestination(targetVector);
            navMeshAgent.speed = enemySpeed;
        }
    }

    /// <summary>
    /// Animation controller.| 1 = Walk. 2 = Run. 3 = Attack.
    /// </summary>
    private void Animate(int an)
    {
        if (an == 1)
        {
            if (enemyType == 1)
            {
                anim.Play("Walk");
            }
            else if (enemyType == 3)
            {
                anim.Play("Walk");
            }
        }

        else if (an == 2)
        {
            if (enemyType == 1)
            {
                anim.Play("Run");
            }
            else if (enemyType == 3)
            {
                anim.Play("Run");
            }
        }

        else if (an == 3)
        {
            if (enemyType == 1)
            {
                anim.Play("Basic Attack");
            }
            else if (enemyType == 3)
            {
                anim.Play("Attack(1)");
            }
        }

        else if (an == 4)
        {
            if (enemyType == 1)
            {
                anim.Play("Die");
            }
            else if (enemyType == 3)
            {
                anim.Play("Die");
            }
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            navMeshAgent.speed = enemySpeed;
        }
    }

    /// <summary>
    /// Checks if creature's health is at 0 or below, then kills it if so.
    /// </summary>
    /// <param name="code"></param>
    public void Die(int code)
    {

        if (code == 1)
        {
            navMeshAgent.speed = 0;
            Animate(4);
            StartCoroutine(RemoveBody(5));
        }

    }

    /// <summary>
    /// Uses FindDistance() to determine whether this creature should attack target gameObject.
    /// </summary>
    public void AttackTarget(Transform target)
    {
        if (FindDistance(transform.transform) <= navMeshAgent.stoppingDistance)
        {
            CharacterStats targetStats = target.GetComponent<CharacterStats>();
            if (targetStats != null)
            {
                navMeshAgent.speed = 0;
                combat.Attack(targetStats);
                Animate(3);
            }
        }
    }

    /// <summary>
    /// Calculates and returns the distance between this enemy and a target object.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public float FindDistance(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance;
    }

    /// <summary>
    /// * Rotates this gameObject to look at target gameObject. |
    /// 1 = Crystal
    /// 2 = Player
    /// </summary>
    /// <param name="target"></param>
    private void FaceTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    /// <summary>
    /// Detects if detectable objects are inside detection radius
    /// </summary>
    public void Detect()
    {
        if (FindDistance(player) <= lookRadius)
        {
            Debug.Log("Detected player.");
            navMeshAgent.SetDestination(player.position);
            if (FindDistance(player) <= navMeshAgent.stoppingDistance)
            {
                anim.StopPlayback();
                hasTarget = true;
                FaceTarget(player);
                AttackTarget(player);
            }
            hasTarget = false;
        }
        if (FindDistance(crystal) <= lookRadius)
        {
            Debug.Log("Detected crystal.");
            navMeshAgent.SetDestination(crystal.position);
            if (FindDistance(crystal) <= navMeshAgent.stoppingDistance)
            {
                hasTarget = true;
                FaceTarget(crystal);
                AttackTarget(crystal);
            }
            hasTarget = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    IEnumerator RemoveBody(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
