using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class PlayerController : MonoBehaviour
{
    //Mostly hook variables used as references in this script
    public Transform playerBody;
    public Transform heldPos;
    Transform heldItem = null;
    public UIManager gameMenu;

    //Flat variables that can be changed in Unity Editor
    public float speed = 10.0f;
    public float jumpForce = 10.0f;

    private float movementV;
    private float movementH;
    private Vector3 lastPosition;

    //Separate variable used as hook for Instructions script. Do not change in Unity editor.
    public bool isHeld = false;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    // Update is called once per frame
    void Update()
    {
        //Sets movement speed for each axis (should be the same)
        movementV = Input.GetAxis("Vertical") * speed; //Forward/backward
        movementH = Input.GetAxis("Horizontal") * speed; //Side to side

        //Stops ability to move when game is paused
        movementV *= Time.deltaTime;
        movementH *= Time.deltaTime;

        //Moves player
        transform.Translate(0, 0, movementV);
        transform.Translate(movementH, 0, 0);

        //Jump function
        if (Input.GetKeyDown("space") && CanJump() && gameMenu.IsPaused() == false)
        {
            GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }

        PlayerPickUp();

        //If item is being held, keep item at heldPos position (essentially just move held item with player)
        if(heldItem != null)
        {
            heldItem.position = heldPos.position;
        }

        PlayerReleaseItem();
    }


    //Checks if player is/isn't in the air, if player is in air then player cannot jump
    bool CanJump()
    {
        Ray ray = new Ray(transform.position, transform.up * -1);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, transform.localScale.y))
            return true;
        else
            return false;
    }
    
    //Detects and picks up items
    void PlayerPickUp()
    {
        //First draws a ray from center of screen
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        //Checks if ray collided with an object
        if (Input.GetButtonDown("Fire2") && Physics.Raycast(ray, out hit)) //Mouse2 button
        {
            if (hit.distance < 5.0f) //Checks distance between player and where the ray hit
            {
                if (hit.transform.tag == "Pickup" && heldItem == null) //Checks if object hit has tag "Pickup" and no object is currently being held by player
                {
                    heldItem = hit.transform;

                    //Stops all movement on object before parenting it
                    heldItem.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    heldItem.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

                    //Makes object child of player
                    heldItem.SetParent(transform);

                    //Following three lines disable physics on heldItem
                    heldItem.GetComponent<Rigidbody>().useGravity = false; 
                    heldItem.GetComponent<Rigidbody>().isKinematic = false;
                    heldItem.GetComponent<Rigidbody>().detectCollisions = false;
                    Debug.Log(hit.transform.position.x + " " + hit.transform.position.y + " " + hit.transform.position.z);

                    //Changes hook variable to true
                    isHeld = true;
                }
            }
        }
    }


    //Drops item if one is being held
    void PlayerReleaseItem()
    {
        if (Input.GetButtonDown("Fire1")) //Mouse1 button
        {
            if (heldItem != null)
            {
                //Following two lines enable physics on heldItem
                heldItem.GetComponent<Rigidbody>().useGravity = true;
                heldItem.GetComponent<Rigidbody>().detectCollisions = true;

                //Remove from player parent
                heldItem.SetParent(null);

                //Adds force to "throw" item
                heldItem.GetComponent<Rigidbody>().AddForce(10, 0, 0);

                //Sets heldItem to empty
                heldItem = null;
            }
        }
    }
}
