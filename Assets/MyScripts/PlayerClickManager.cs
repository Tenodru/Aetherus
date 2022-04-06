using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClickManager : MonoBehaviour
{
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Returns ID of interactable clicked by player.
    /// </summary>
    /// <returns></returns>
    public int CheckForHit()
    {
        Ray centerRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Clicked lmouse button.");
            if (CheckCenterRay(centerRay, 5))
            {
                if (hit.transform.tag == "StartWave")
                {
                    Debug.Log("Clicked Start Wave button.");
                    return 1;
                }
                else return 0;
            }
            else return 0;
        }
        else return 0;
    }

    /// <summary>
    /// Checks if a ray hit within a distance.
    /// </summary>
    /// <returns></returns>
    public bool CheckCenterRay(Ray ray, int distance)
    {
        if (Physics.Raycast(ray, out hit) && hit.distance <= distance)
        {
            return true;
        }
        else return false;
    }

    /// <summary>
    /// Checks if hit target has tag "tag."
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public bool CheckHitTag(Ray ray, string tag)
    {
        if (Physics.Raycast(ray, out hit) && hit.transform.tag == tag)
        {
            return true;
        }
        else return false;
    }
}
