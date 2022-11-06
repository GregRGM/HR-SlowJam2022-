using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Wave : MonoBehaviour
{
    public SpawnPoint[] spawnPoints;
    public GameObject enemy;
    public int count;
    public float rate;

}