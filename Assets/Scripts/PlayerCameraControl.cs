using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public enum CombatState {
    COMBAT, // Fighting enemies
    NAVIGATION, // Navigating to fight enemies
}

// Moves the player from room to other room.
public class PlayerCameraControl : MonoBehaviour
{
    public CinemachineVirtualCamera combatCam;

    public CombatState combatState;

    public void SetCameraDetails(Transform cameraPos, Transform toLookAt)
    {
        combatCam.transform.position = cameraPos.position;
        combatCam.m_LookAt = toLookAt;
    }

    public void SetCombatState(CombatState c) 
    {
        combatState = c;
    }

    void Update () {
        if (combatState == CombatState.NAVIGATION)
        {
            Cursor.lockState = CursorLockMode.Locked;
            float mouseInput = Input.GetAxis("Mouse X");
            Vector3 lookhere = new Vector3(0,mouseInput,0);
            transform.Rotate(lookhere);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
