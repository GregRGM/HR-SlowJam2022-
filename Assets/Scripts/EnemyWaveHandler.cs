using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// yeah this is based off Brackeys code
public enum WaveState {
    SPAWNING, // Spawning enemies
    WAITING, // Waiting for enemies to die
    COUNTING // Countdown to next wave
}

public class EnemyWaveHandler : MonoBehaviour
{
    public Wave[] waves;
    public WaveState waveState = WaveState.COUNTING;
    int waveNumber = 0;
    public float timeToWait = 5f;
    float waveCountdown;
    float timeRate = 1f;

    void Start()
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
                    Debug.Log("New wave");
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
        Debug.Log("Final wave clear! Go to next room or win game");
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        waveState = WaveState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.GetRandomEnemy(), _wave.GetRandomSpawnPoint());
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        waveState = WaveState.WAITING;

        yield break;
    }

    void SpawnEnemy(GameObject _enemy, SpawnPoint point) 
    {
        Debug.Log("Spawning enemy");
        point.Spawn(_enemy);
    }
}
