using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// Controller for player attacks.
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    public FirstPersonController playerController;
    public PlayerClickManager clickManager;
    public Interactable focus;

    private float distance;
    private int damage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectInteract();
        }
    }

    /// <summary>
    /// Detects if there is an interactable object.
    /// </summary>
    private void DetectInteract()
    {
        Debug.Log("Clicked left mouse button.");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            Interactable interactable = hit.transform.GetComponent<Interactable>();
            if (interactable != null && Vector3.Distance(transform.position, interactable.transform.position) < interactable.radius)
            {
                Debug.Log("Detected interactable.");
                SetFocus(interactable);
            }
        }
    }

    /// <summary>
    /// Sets current focused interactable.
    /// </summary>
    /// <param name="newFocus"></param>
    private void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }
            focus = newFocus;
        }
        newFocus.OnFocused(transform);
    }

    /// <summary>
    /// Removes current focused interactable.
    /// </summary>
    private void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
    }

    /// <summary>
    /// Basic attack manager.
    /// </summary>
    private void BasicAttack()
    {
        Ray centerRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        Debug.Log("Clicked m1.");
        if (Physics.Raycast(centerRay, out hit) && hit.distance <= 10)
        {
            Debug.Log(hit.collider);
            Debug.Log("Hit detected.");
            if (hit.transform.tag == "Enemy1")
            {
                hit.transform.GetComponent<Health>().ChangeHealth(-5);
                Debug.Log("Basic attack.");
            }
        }
    }

    /// <summary>
    /// Get damage stat.
    /// </summary>
    /// <returns></returns>
    public int Damage()
    {
        return damage;
    }

    /// <summary>
    /// Add to damage stat.
    /// </summary>
    /// <param name="add"></param>
    public void ChangeDamage(int change)
    {
        damage += change;
        Debug.Log("Changed damage.");
    }
}
