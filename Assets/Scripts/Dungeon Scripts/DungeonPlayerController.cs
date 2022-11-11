using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonPlayerController : MonoBehaviour
{
    public bool smoothTransition = false;
    public float transitionSpeed = 10f;
    public float transitionRotationSpeed = 500f;
    public float movementScale = 50f;

    Vector3 targetGridPos;
    Vector3 prevTargetGridPos;
    Vector3 targetRotation;

    public void RotateLeft() 
    {
        if (AtRest)
        {
            targetRotation -= Vector3.up * 90f;
        }
    }

    public void RotateRight()
    {
        if (AtRest)
        {
            targetRotation += Vector3.up * 90f;
        }
    }

    public void MoveForward()
    {
        if (AtRest)
        {
            targetGridPos += transform.forward * movementScale;
        }
    }

    public void MoveBackward()
    {
        if (AtRest)
        {
            targetGridPos -= transform.forward * movementScale;
        }
    }

    public void MoveLeft()
    {
        if (AtRest)
        {
            targetGridPos -= transform.right * movementScale;
        }
    }

    public void MoveRight()
    {
        if (AtRest)
        {
            targetGridPos += transform.right * movementScale;
        }
    }

    bool AtRest
    {
        get
        {
            if (Vector3.Distance(transform.position, targetGridPos) < 0.05f && Vector3.Distance(transform.eulerAngles, targetRotation) < 0.05f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        targetGridPos = Vector3Int.RoundToInt(transform.position);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // Check if player can move to target position
        if (true)
        {
            prevTargetGridPos = targetGridPos;

            Vector3 targetPosition = targetGridPos;

            if (targetRotation.y > 270f && targetRotation.y < 361f)
            {
                targetRotation.y = 0f;
            }
            if (targetRotation.y < 0f)
            {
                targetRotation.y = 270f;
            }

            if (!smoothTransition)
            {
                transform.position = targetPosition;
                transform.rotation = Quaternion.Euler(targetRotation);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * transitionSpeed);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * transitionRotationSpeed);
            }
        }
        else
        {
            targetGridPos = prevTargetGridPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
