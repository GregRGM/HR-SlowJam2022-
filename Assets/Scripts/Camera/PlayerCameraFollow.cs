using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameraFollow : MonoBehaviour
{
    public CinemachineVirtualCamera PlayerFollowCam;

    private void Awake()
    {
        CameraRoomSwitcher.Register(PlayerFollowCam);
        CameraRoomSwitcher.SwitchCamera(PlayerFollowCam);
    }

    public void FollowPlayer()
    {
        CameraRoomSwitcher.SwitchCamera(PlayerFollowCam);
    }
}
