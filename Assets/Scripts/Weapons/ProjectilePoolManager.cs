using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolManager : MonoBehaviour
{
    public static ProjectilePoolManager instance;

    private Queue<GameObject> pooledSingleProjectileObjects = new Queue<GameObject>();
    //I can make this serialized if this needs to be adjusted
    [SerializeField] private int _amountToPool = 200;

    [SerializeField] private GameObject _SingleProjectilePrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            GameObject obj = Instantiate(_SingleProjectilePrefab);
            obj.SetActive(false);
            pooledSingleProjectileObjects.Enqueue(obj);
        }
    }

    public GameObject GetPooledSingleProjectileObj()
    {
        if (pooledSingleProjectileObjects.Count > 0)
        {
            GameObject obj = pooledSingleProjectileObjects.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(_SingleProjectilePrefab);
            return obj;
        }
    }

    public void ReturnSingleProjectile(GameObject projectile)
    {
        pooledSingleProjectileObjects.Enqueue(projectile);
        projectile.SetActive(false);
    }
}
