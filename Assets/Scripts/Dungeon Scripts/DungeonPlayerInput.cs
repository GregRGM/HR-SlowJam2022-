using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputState {
    ROTATELEFT,
    ROTATERIGHT,
    FORWARD,
    BACKWARD,
    LEFT,
    RIGHT,
    NONE
}

[RequireComponent(typeof(DungeonPlayerController))]
public class DungeonPlayerInput : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;

    float moveThreshold = 0.9f;

    public bool restricted;

    DungeonPlayerController controller;
    InputState lastInput = InputState.NONE;

    private void Awake()
    {
        controller = GetComponent<DungeonPlayerController>();
    }

    // Start is called before tAhe first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (restricted) return;

        if (horizontalInput < -moveThreshold && lastInput != InputState.ROTATELEFT) 
        {
            controller.RotateLeft();
            lastInput = InputState.ROTATELEFT;
        }
        else if (horizontalInput > moveThreshold && lastInput != InputState.ROTATERIGHT) 
        {
            controller.RotateRight();
            lastInput = InputState.ROTATERIGHT;
        }
        else if (verticalInput > moveThreshold && lastInput != InputState.FORWARD)
        {
            controller.MoveForward();
            lastInput = InputState.FORWARD;
        }
        else if (verticalInput < -moveThreshold && lastInput != InputState.BACKWARD)
        {
            controller.MoveBackward();
            lastInput = InputState.BACKWARD;
        }
        else
        {
            if (Mathf.Abs(verticalInput) < moveThreshold && Mathf.Abs(horizontalInput) < moveThreshold)
            {
                lastInput = InputState.NONE;
            }
        }
    }


}
