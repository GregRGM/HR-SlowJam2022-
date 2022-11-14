using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnEnable()
    {
        _Player = FindObjectOfType<PlayerFiringPlatform>().gameObject;
    }
    [ContextMenu("Start Room")]
    public void StartRoom()
    {
        roomWaveHandler.gameObject.SetActive(true);
        roomWaveHandler.SetOwner(this);
        //_Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        _Player.GetComponent<Animator>().Play("Combat");
        _Player.GetComponent<PlayerCameraControl>().SetCameraDetails(cameraPosition, screenLookAt);
        if (axisToFreeze == FreezeAxisType.X)
        {
            _Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
            _Player.transform.rotation = Quaternion.LookRotation(facing);
        }
        if (axisToFreeze == FreezeAxisType.Y)
        {
            _Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            _Player.transform.rotation = Quaternion.LookRotation(facing);
        }
        if (axisToFreeze == FreezeAxisType.Z)
        {
            _Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            _Player.transform.rotation = Quaternion.LookRotation(facing);
        }
    }
    public void EndRoom()
    {
        roomWaveHandler.gameObject.SetActive(false);
        _Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        _Player.GetComponent<Animator>().Play("DungeonTravel3D");
        Debug.Log("Ended Room you can explore it now");
    }

    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        if (!roomWaveHandler.IsDone) {
            StartRoom();
        }
    }
}

public enum FreezeAxisType
{
    X, Y, Z
}
