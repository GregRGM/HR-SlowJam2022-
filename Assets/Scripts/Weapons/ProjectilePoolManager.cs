using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolManager : MonoBehaviour
{
    public static ProjectilePoolManager instance;

    private Queue<GameObject> pooledSinglePlayerProjectileObjects = new Queue<GameObject>();
    private Queue<GameObject> pooledSingleEnemyProjectileObjects = new Queue<GameObject>();
    //I can make this serialized if this needs to be adjusted
    [SerializeField] private int _amountToPoolPlayer = 100;
    [SerializeField] private int _amountToPoolEnemy = 100;

    [SerializeField] private GameObject _SinglePlayerProjectile;
    [SerializeField] private GameObject _SingleEnemyProjectile;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < _amountToPoolPlayer; i++)
        {
            GameObject obj = Instantiate(_SinglePlayerProjectile);
            obj.SetActive(false);
            pooledSinglePlayerProjectileObjects.Enqueue(obj);
        }
        for (int i = 0; i < _amountToPoolEnemy; i++)
        {
            GameObject obj = Instantiate(_SingleEnemyProjectile);
            obj.SetActive(false);
            pooledSingleEnemyProjectileObjects.Enqueue(obj);
        }
    }

    public GameObject GetPooledPlayerSingleProjectileObj()
    {
        if (pooledSinglePlayerProjectileObjects.Count > 0)
        {
            GameObject obj = pooledSinglePlayerProjectileObjects.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(_SinglePlayerProjectile);
            return obj;
        }
    }

    public void ReturnPlayerSingleProjectile(GameObject projectile)
    {
        pooledSinglePlayerProjectileObjects.Enqueue(projectile);
        projectile.SetActive(false);
    }

    public GameObject GetPooledEnemySingleProjectileObj()
    {
        if (pooledSingleEnemyProjectileObjects.Count > 0)
        {
            GameObject obj = pooledSingleEnemyProjectileObjects.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(_SingleEnemyProjectile);
            return obj;
        }
    }

    public void ReturnEnemySingleProjectile(GameObject projectile)
    {
        pooledSingleEnemyProjectileObjects.Enqueue(projectile);
        projectile.SetActive(false);
    }
}
