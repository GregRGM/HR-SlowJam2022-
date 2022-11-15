using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomScript : MonoBehaviour
{
    public int id;
    public Transform startingPoint;
    public bool isFinalRoom; // Indicates wave cleared if room is cleared
    public Transform cameraPosition;
    public Transform screenLookAt;
    public EnemyWaveHandler roomWaveHandler;
    public GameObject _Player;
    public FreezeAxisType axisToFreeze;
    public Vector3 facing;

    public CinemachineVirtualCamera RoomCamera;

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
        roomWaveHandler.SetOwner(this);
        CameraRoomSwitcher.SwitchCamera(RoomCamera);

        //_Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        //_Player.GetComponent<Animator>().Play("Combat");
        // _Player.GetComponent<PlayerCameraControl>().SetCameraDetails(cameraPosition, screenLookAt);
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
       // _Player.GetComponent<Animator>().Play("DungeonTravel3D");
        Debug.Log("Ended Room you can explore it now");
    }

    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!roomWaveHandler.IsDone)
            {
                StartRoom();
            }
        } 
    }
}

public enum FreezeAxisType
{
    X, Y, Z
}
