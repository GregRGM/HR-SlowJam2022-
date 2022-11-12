using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Wave : MonoBehaviour
{
    public SpawnPoint[] spawnPoints;
    public GameObject[] enemys;
    [Tooltip("Number to spawn")]
    public int count;
    [Tooltip("Time between spawns")]
    public float rate;


    public GameObject GetRandomEnemy()
    {
        int t = UnityEngine.Random.RandomRange(0, enemys.Length);
        return enemys[t];
    }
    public SpawnPoint GetRandomSpawnPoint()
    {
        int t = UnityEngine.Random.RandomRange(0, spawnPoints.Length);
        return spawnPoints[t];
    }
}