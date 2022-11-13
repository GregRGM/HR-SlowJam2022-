using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

// Moves the player from room to other room.
public class RoomMovement : MonoBehaviour
{
    public float transitionSpeed = 10f; // Should be the same as movement speed
    public float transitionRotationSpeed = 500f;

    Dictionary<int, RoomScript> idToRoom = new Dictionary<int, RoomScript>();

    public RoomScript currentRoom;
    public CinemachineVirtualCamera vcam;

    Vector3 targetPosition;
    Vector3 prevtargetPosition;
    Vector3 targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject roomObject in GameObject.FindGameObjectsWithTag("Room"))
        {
            RoomScript r = roomObject.GetComponent<RoomScript>();
            idToRoom[r.id] = r;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator MoveToRoom(int roomId)
    {
        prevtargetPosition = targetPosition;

        if (!idToRoom.TryGetValue(roomId, out RoomScript room)) {
           Debug.Log("Tried going to invalid room");
           yield break;
        }

        targetPosition = room.startingPoint.position;
        targetRotation = room.startingPoint.eulerAngles;

        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.enabled = true;
        agent.destination = targetPosition;

        Debug.Log(agent.destination);
        Debug.Log(transform.position);

        vcam.m_LookAt = room.screenLookAt;
        
        while (!AtRest)
        {
            // Check if player can move to target position
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * transitionRotationSpeed);

            yield return null;
        }

        agent.enabled = false;
        currentRoom = room;
        yield break;
    }

    bool AtRest
    {
        get
        {
            if (Vector3.Distance(transform.position, targetPosition) < 1f && Vector3.Distance(transform.eulerAngles, targetRotation) < 0.05f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
