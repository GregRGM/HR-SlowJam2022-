using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFire : BaseAttack
{

    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _SpawnOrigin;

    [SerializeField] AudioClip fireballShotSFX;
    AudioSource audioSource;
    [SerializeField] private float _fireRate;
    private float time = 0f;

    private void Update()
    {
        time += Time.deltaTime;
    }
    public override void FireAttack()
    {
        if (time > _fireRate)
        {
            time = 0f;
            LaunchProjectile();
        }
    }
    public void LaunchProjectile()
    {
        GameObject projectile = ProjectilePoolManager.instance.GetPooledEnemySingleProjectileObj();
        projectile.transform.position = _SpawnOrigin.position;
        projectile.transform.rotation = Quaternion.LookRotation(_SpawnOrigin.forward, Vector3.up);
        PlayShootSound(fireballShotSFX);
    }
    private void PlayShootSound(AudioClip _audio)
    {
        AudioSource.PlayClipAtPoint(_audio, transform.position);
    }
}
