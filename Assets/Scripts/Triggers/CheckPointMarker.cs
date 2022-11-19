using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointMarker : MonoBehaviour
{
    [SerializeField] private CheckPointManager manager;

    private void Start()
    {
        manager.RegisterCheckPoint(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            manager.UpdateCheckpoint(this);
        }
        
    }
}
