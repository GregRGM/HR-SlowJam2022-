using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public int id;
    public Transform startingPoint;
    public bool isFinalRoom; // Indicates wave cleared if room is cleared
    public Transform screenLookAt;
    public EnemyWaveHandler roomWaveHandler;
    public GameObject _Player;
    public FreezeAxisType axisToFreeze;
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
    }
    public void EndRoom()
    {
        roomWaveHandler.gameObject.SetActive(false);
        _Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        Debug.Log("Ended Room you can explore it now");
    }
}

public enum FreezeAxisType
{
    X, Y, Z
}
