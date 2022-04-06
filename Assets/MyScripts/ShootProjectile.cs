using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller to fire projectiles.
/// </summary>
public class ShootProjectile : MonoBehaviour
{
    public GameObject towerProj;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    /// <summary>
    /// Fires projectile at specified target.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    /// <param name="par"></param>
    public void FireProj(Transform target, Vector3 pos, Quaternion rot, Transform par)
    {
        Instantiate(towerProj, pos, rot, par);
        Instantiate(towerProj, pos, rot, par).GetComponent<ProjectileAI>().SetTarget(target);
        Debug.Log("Projectile following.");
    }
}
