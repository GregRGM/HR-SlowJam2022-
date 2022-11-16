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

    private int spawnIndex;

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

    public SpawnPoint GetNextSpawnPoint()
    {
        if (spawnIndex == spawnPoints.Length)
        {
            spawnIndex = 0;
            Debug.Log("I failed you");
            return spawnPoints[spawnIndex];
        }
        SpawnPoint savedpoint = spawnPoints[spawnIndex];
        spawnIndex++;
        Debug.Log("I failed you");
        return savedpoint;
    }
}