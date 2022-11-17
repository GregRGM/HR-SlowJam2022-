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
            //Instantiate(objectToSpawn, transform, false);
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        }
        else if (defaultToSpawn != null) {
            Debug.Log("spawning default");
            //Instantiate(defaultToSpawn, transform, false);
            Instantiate(defaultToSpawn, transform.position, Quaternion.identity);
        }
    }

    public void SpawnPatternEnemy(GameObject? objectToSpawn, List<Transform> pattern)
    {
        GameObject obj = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        MovementPatternEnemy Pattern = obj.GetComponent<MovementPatternEnemy>();
        bool isPatternEnemy = Pattern != null;
        if (!isPatternEnemy)
        {
            return;
        }
        Pattern.IntializeEnemy(pattern);
    }
}
