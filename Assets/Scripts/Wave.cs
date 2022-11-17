using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Wave : MonoBehaviour
{
    public SpawnPoint[] spawnPoints;
    public GameObject[] enemys;
    [Tooltip("Places to move between")]
    public PatrolPattern[] patrols;
    [Tooltip("Number to spawn")]
    public int count;
    [Tooltip("Time between spawns")]
    public float rate;
    public bool isStackingWave;
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
            return spawnPoints[spawnIndex];
        }
        SpawnPoint savedpoint = spawnPoints[spawnIndex];
        spawnIndex++;
        return savedpoint;
    }

    public PatrolPattern GetPatrolPattern()
    {
        int t = UnityEngine.Random.RandomRange(0, patrols.Length);
        PatrolPattern newpattern = patrols[t];
        return newpattern;
    }
}

[Serializable]
public class PatrolPattern
{
    public List<Transform> pattern = new List<Transform>();
}