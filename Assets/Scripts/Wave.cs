using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Wave : MonoBehaviour
{
    public SpawnPoint[] spawnPoints;
    public GameObject[] enemys;

    // TODO change to array of gameobjects (the parent objects to wave points)
    [Tooltip("Places to move between (as game objects that are parents of a set of points)")]
    public GameObject[] patrolParents;

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

        int t = UnityEngine.Random.RandomRange(0, patrolParents.Length);
        PatrolPattern newpattern = GetPatrolPattern(patrolParents[t]);
        return newpattern;
    }

    public PatrolPattern GetPatrolPattern(GameObject patrolParent)
    {
        PatrolPattern p = new PatrolPattern();
        foreach (Transform child in patrolParent.transform)
        {
            p.pattern.Add(child);
        }
        return p;
    }
}

[Serializable]
public class PatrolPattern
{
    public List<Transform> pattern = new List<Transform>();
}