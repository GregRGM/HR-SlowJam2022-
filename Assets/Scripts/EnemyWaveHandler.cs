using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// yeah this is based off Brackeys code 
//we stan Brackeys - SG
public enum WaveState {
    SPAWNING, // Spawning enemies
    WAITING, // Waiting for enemies to die
    COUNTING // Countdown to next wave
}

public class EnemyWaveHandler : MonoBehaviour
{
    public Wave[] waves;
    public WaveState waveState = WaveState.COUNTING;

    RoomScript _owner;
    CombatRoom _cowner;
    public int nextRoomId;
    int waveNumber = 0;
    public float timeToWait = 5f;
    float waveCountdown;
    float timeRate = 1f;

    public bool IsDone 
    {
        get 
        {
            return waveNumber >= waves.Length;
        }
    }
    private void OnEnable()
    {
        waveNumber = 0;
        waveCountdown = timeToWait;
    }

    void Update()
    {
        switch (waveState) {
            case WaveState.COUNTING:
                waveCountdown -= Time.deltaTime * timeRate;
                if (waveCountdown <= 0) {
                    Debug.Log("Start spawning enemies");
                    if (waveNumber < waves.Length)
                    {
                        StartCoroutine(SpawnWave(waves[waveNumber]));
                    }
                    else
                    {
                        OnLastWaveClear();
                    }
                    waveCountdown = timeToWait;
                }
                break;
            case WaveState.SPAWNING:
                // TODO have code that runs while enemies spawning
                break;
            case WaveState.WAITING:
                // TODO wait for enemies to die
                if (!EnemiesAreAlive())
                {
                    waveNumber += 1;

                    waveState = WaveState.COUNTING;
                    //Debug.Log("New wave");
                }
                break;
        }
    }

    bool EnemiesAreAlive() {
        return (GameObject.FindGameObjectsWithTag("Enemy").Length > 0);
    }

    // Code to run once the last wave is done.
    void OnLastWaveClear() 
    {
        Debug.Log("Finished this room");
        //var player = GameObject.FindGameObjectsWithTag("Player").First();
        //_owner.EndRoom();
        _cowner.EndRoom();
        //if (player.GetComponent<RoomMovement>().currentRoom.isFinalRoom)
        //{
        //    Debug.Log("Level complete!");
        //}
        //else
        //{
        //    //StartCoroutine(player.GetComponent<RoomMovement>().MoveToRoom(nextRoomId));
        //    waveNumber = 0;

        //    // TODO reset waves
        //}
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        waveState = WaveState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.GetRandomEnemy(), _wave.GetNextSpawnPoint(), _wave.GetPatrolPattern().pattern);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        waveState = WaveState.WAITING;

        yield break;
    }

    void SpawnEnemy(GameObject _enemy, SpawnPoint point, List<Transform> pattern) 
    {
        //Debug.Log("Spawning enemy");
        MovementPatternEnemy obj = _enemy.GetComponent<MovementPatternEnemy>();
        if (obj!=null)
        {
            point.SpawnPatternEnemy(_enemy, pattern);
        }
        else
        {
            point.Spawn(_enemy);
        }
        
    }

    public void SetOwner(RoomScript room)
    {
        _owner = room;
    }
    public void SetCombatOwner(CombatRoom room)
    {
        _cowner = room;
    }
}
