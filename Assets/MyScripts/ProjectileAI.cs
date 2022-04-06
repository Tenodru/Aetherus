using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Projectile AI controller.
/// </summary>

public class ProjectileAI : MonoBehaviour
{

    private Transform targetObject;
    private Rigidbody rigidBody;

    private float speed = 10;
    private float rotationSpeed = 2;
    private float focusDistance = 5;
    private bool isFollowingTarget = true;
    private bool isFacingTarget = false;
    private Vector3 tempVector;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        FollowTarget(targetObject.transform);
    }

    public void SetTarget(Transform target)
    {
        targetObject = target;
    }

    public void FollowTarget(Transform target)
    {
        if (Vector3.Distance(transform.position, target.position) < focusDistance)
        {
            isFollowingTarget = false;
        }

        Vector3 targetDirection = target.position - transform.position;

        if (isFacingTarget)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0.0F);

            MoveForward(Time.deltaTime);

            if (isFollowingTarget)
            {
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }
        else
        {
            if (isFollowingTarget)
            {
                tempVector = targetDirection.normalized;

                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(tempVector * Time.deltaTime * speed, Space.World);
            }
        }

    }

    /// <summary>
    /// Moves forward at 'speed' multiplied by 'rate', per frame. 
    /// Use Time.deltaTime as a parameter to travel forward at the same speed per second.
    /// </summary>
    /// <param name="rate"></param>
    private void MoveForward(float rate)
    {
        transform.Translate(Vector3.forward * rate * speed, Space.Self);
    }
}
