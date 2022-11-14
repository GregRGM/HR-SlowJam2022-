using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

// Moves the player from room to other room.
public class PlayerCameraControl : MonoBehaviour
{
    public CinemachineVirtualCamera combatCam;

    public void SetCameraDetails(Transform cameraPos, Transform toLookAt)
    {
        combatCam.transform.position = cameraPos.position;
        combatCam.m_LookAt = toLookAt;
    }
}
