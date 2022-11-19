using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField] private List<CheckPointMarker> _checkPoints = new List<CheckPointMarker>();
    private CheckPointMarker _currentCheckpoint;

    private void Awake()
    {
        _checkPoints.Clear();
    }

    public void UpdateCheckpoint(CheckPointMarker newcheckpoint)
    {
        _currentCheckpoint = newcheckpoint;
    }

    public void RespawnPlayerAtCheckPoint(GameObject obj)
    {
        obj.transform.position = _currentCheckpoint.gameObject.transform.position;
        obj.transform.rotation = _currentCheckpoint.gameObject.transform.rotation;
    }

    public void RegisterCheckPoint(CheckPointMarker newcheckpoint)
    {
        _checkPoints.Add(newcheckpoint);
    }
}
