using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CombatRoom : MonoBehaviour
{
    [SerializeField] private bool isFinalRoom; // Indicates wave cleared if room is cleared
    [SerializeField] private EnemyWaveHandler roomWaveHandler;
    private GameObject _Player;
    [SerializeField] private FreezeAxisType axisToFreeze;
    [SerializeField] private CinemachineVirtualCamera RoomCamera;

    private void OnEnable()
    {
        _Player = FindObjectOfType<PlayerFiringPlatform>().gameObject;
        CameraRoomSwitcher.Register(RoomCamera);
    }
    private void OnDisable()
    {
        CameraRoomSwitcher.UnRegister(RoomCamera);
    }
    [ContextMenu("Start Room")]
    public void StartRoom()
    {
        roomWaveHandler.gameObject.SetActive(true);
        roomWaveHandler.SetCombatOwner(this);
        CameraRoomSwitcher.SwitchCamera(RoomCamera);

        if (axisToFreeze == FreezeAxisType.X)
        {
            _Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
        }
        if (axisToFreeze == FreezeAxisType.Y)
        {
            _Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }
        if (axisToFreeze == FreezeAxisType.Z)
        {
            _Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
        _Player.transform.rotation = Quaternion.LookRotation(RoomCamera.transform.forward);
    }
    public void EndRoom()
    {
        roomWaveHandler.gameObject.SetActive(false);
        _Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        _Player.GetComponent<PlayerCameraFollow>().FollowPlayer();
    }
}

