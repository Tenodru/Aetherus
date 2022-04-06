using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tower AI controller.
/// </summary>
public class TowerAI : MonoBehaviour
{
    [Header ("Attributes")]
    [SerializeField] int towerCode = 0;
    [SerializeField] int range = 10;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header ("Script Setup")]
    private bool hasTarget;
    private bool canAttack;
    private Transform targetedEnemy;
    [SerializeField] GameObject projectile;
    private Transform firePoint;


    // Start is called before the first frame update
    void Start()
    {
        hasTarget = false;
        canAttack = true;

        //transform.GetChild(0).gameObject.SetActive(false);
        //InvokeRepeating("UpdateEnemy()", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (targetedEnemy == null)
        {
            return;
        }
    }

    private void UpdateEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy1");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            targetedEnemy = nearestEnemy.transform;
        }
        else targetedEnemy = null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy1" && canAttack == true)
        {

        }
    }

    private void TargetEnemy()
    {
        Vector3 direction = targetedEnemy.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = lookRotation.eulerAngles;

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown = Time.deltaTime;
    }

    private void Shoot()
    {

    }

    /*
    private void AttackEnemy(GameObject target)
    {
        StartCoroutine(Shoot(5, target));
    }

    IEnumerator Shoot(int time, GameObject target)
    {
        Vector3 tPosition = new Vector3(transform.position.x + 0.5f, transform.position.y + 3.0f, transform.position.z - 0.5f);
        Debug.Log("Attacked.");
        projAI.FireProj(target.transform, tPosition, transform.rotation, transform);
        canAttack = false;
        yield return new WaitForSeconds(time);
        canAttack = true;
    }*/
}
