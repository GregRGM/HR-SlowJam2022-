using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Some code for a spawn point
public class SpawnPoint : MonoBehaviour
{
    public GameObject defaultToSpawn;
    
    // Code to spawn an enemy
    public void Spawn(GameObject? objectToSpawn) {
        if (objectToSpawn != null) {
            Instantiate(objectToSpawn, transform);
        }
        else if (defaultToSpawn != null) {
            Instantiate(defaultToSpawn, transform);
        }
    }
}
