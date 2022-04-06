using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Controller for health display elements on gameObjects.
/// </summary>
public class HealthDisplay : MonoBehaviour
{
    public Health health;

    // Start is called before the first frame update
    void Start()
    {
        health = this.transform.GetComponentInParent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Changes the health display of this object.
    /// </summary>
    public void ChangeHealthDisplay()
    {
        this.GetComponent<TextMeshPro>().text = health.GetHealth().ToString();
        Debug.Log("Changed health display.");
    }
}
