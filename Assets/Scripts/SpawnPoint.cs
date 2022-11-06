using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Some code for a spawn point
[System.Serializable]
public class SpawnPoint : MonoBehaviour
{
    public GameObject defaultToSpawn;
    
    // Code to spawn an enemy
    public void Spawn(GameObject? objectToSpawn) 
    {
        if (objectToSpawn != null) {
            Debug.Log("spawning object to spawn");
            Instantiate(objectToSpawn, transform);
        }
        else if (defaultToSpawn != null) {
            Debug.Log("spawning default");
            Instantiate(defaultToSpawn, transform);
        }
    }
}
