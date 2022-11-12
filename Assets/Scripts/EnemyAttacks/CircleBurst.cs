using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBurst : BaseAttack
{
    [Header("References")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _SpawnOrigin;
 
    [Header("Variables")]
    
    [SerializeField] [Range(3, 15)] private int _vertices = 3;
    [SerializeField] [Range(0.5f, 4f)] private float _radius = 4f;
    const float TAU = 6.283185307179586f;

    [Header("Feedback")]
    [SerializeField] private AudioClip _shotSound;

    public bool IsBurst = true;

    [SerializeField] private float _fireRate;
    private float time = 10f;

    private void Update()
    {
        time += Time.deltaTime;
    }
    public override void FireAttack()
    {
        if (time > _fireRate)
        {
            time = 0f;
            ShootBurst();
        }
    }

    private List<Vector3> _spawnPoints = new List<Vector3>();

    private void CalculatePoints()
    {
        _spawnPoints.Clear();
        for (int i = 0; i < _vertices; i++)
        {
            float t = i / (float)_vertices;
            float AngleNRads = t * TAU;
            float xCord = Mathf.Cos(AngleNRads);
            float yCord = Mathf.Sin(AngleNRads);

            Vector3 Dir = new Vector3(xCord, yCord, 0f);
            Vector3 point = Dir * _radius;
            Vector3 WorldSpacePoint = new Vector3();
            WorldSpacePoint = _SpawnOrigin.TransformPoint(point);
            _spawnPoints.Add(WorldSpacePoint);
        }
    }

    private void SpawnSpreadProjectilesAtPoints()
    {
        foreach (var point in _spawnPoints)
        {
            GameObject projectile = ProjectilePoolManager.instance.GetPooledEnemySingleProjectileObj();
            projectile.transform.position = point;
            //aiming forward
            projectile.transform.rotation = Quaternion.LookRotation(_SpawnOrigin.forward, Vector3.up);
            PlayShootSound(_shotSound);
        }
    }

    private void SpawnBurstProjectilesAtPoints()
    {

    }

    private void PlayShootSound(AudioClip _audio)
    {
        AudioSource.PlayClipAtPoint(_audio, transform.position);
    }

    [ContextMenu("Shoot Burst")]
    public void ShootBurst()
    {
        CalculatePoints();
        SpawnSpreadProjectilesAtPoints();
    }
}
