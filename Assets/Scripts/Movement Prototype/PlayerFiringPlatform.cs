
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

enum WeaponSelection
{
    Fireball,
    Spread,
    Laser
}

public class PlayerFiringPlatform : MonoBehaviour
{
    [SerializeField] private LayerMask AimColliderLayerMask;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject laserObject;
    [SerializeField] private Transform spawnPoint;
    public Transform hitpoint;
    private Vector3 mouseWorldPosition;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask ignoreLayer;
    private bool isFiring;

    [SerializeField] private float fireballShotRate = 60, spreadShotRate = 30, laserHitRate = 1f;

    [SerializeField] private WeaponSelection weaponSelection;

    [SerializeField] AudioClip fireballShotSFX, spreadShotSFX, laserShotSFX;
    AudioSource audioSource;


    //private float time = 0f;

    private void Update()
    {
        mouseWorldPosition = Vector3.zero;

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, AimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
            hitpoint.position = raycastHit.point;
            //Debug.Log(mouseWorldPosition);
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            weaponSelection += Mathf.FloorToInt(Input.GetAxis("Mouse ScrollWheel") * 10);
            if(weaponSelection < 0)
                weaponSelection = WeaponSelection.Laser;    
            else if(weaponSelection > WeaponSelection.Laser)
                weaponSelection = WeaponSelection.Fireball;
        }

        if(Input.mouseScrollDelta.y < 0)
        {
            //(int) weaponSelection -= mouseScrollDelta.y;
            if(weaponSelection < 0)
            {
                weaponSelection = WeaponSelection.Laser;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            //time += Time.deltaTime;

            switch (weaponSelection)
            {
                case WeaponSelection.Fireball:
                    {
                        //if(time > fireballShotRate)
                        //    FireShot();
                        //time = 0f;
                        InvokeRepeating("FireShot", 0f, 1 / fireballShotRate);
                    }
                    break;
                case WeaponSelection.Spread:
                    {
                        InvokeRepeating("FireSpread", 0f, 1 / spreadShotRate);
                    }
                    break;
                case WeaponSelection.Laser:
                    {
                        FireLaser();
                    }
                    break;
                default:
                    break;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            switch (weaponSelection)
            {
                case WeaponSelection.Fireball:
                    {
                        CancelInvoke("FireShot");
                    }
                    break;
                case WeaponSelection.Spread:
                    {
                        CancelInvoke("FireSpread");
                    }
                    break;
                case WeaponSelection.Laser:
                    {
                        laserObject.GetComponent<LaserHandler>().ToggleLaser(false);
                    }
                    break;
                default:
                    break;
            }
        }

    }

    private void PlayShootSound(AudioClip _audio)
    {
        AudioSource.PlayClipAtPoint(_audio, transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(spawnPoint.position, hitpoint.position);
    }
    private void FireShot()
    {
        Vector3 AimDirection = (mouseWorldPosition - spawnPoint.position).normalized;
        Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(AimDirection, Vector3.up));
        PlayShootSound(fireballShotSFX);
    }
    
    private void FireSpread()
    {
        Vector3 AimDirection = (mouseWorldPosition - spawnPoint.position).normalized;
        Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(AimDirection, Vector3.up));
        Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(new Vector3(AimDirection.x * 1.5f, AimDirection.y * 1.5f, AimDirection.z), Vector3.up));
        Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(new Vector3(AimDirection.x * 1.3f, AimDirection.y * 1.3f, AimDirection.z), Vector3.up));
        Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(new Vector3(AimDirection.x * .7f, AimDirection.y * .7f, AimDirection.z), Vector3.up));        
        Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(new Vector3(AimDirection.x * .5f, AimDirection.y * .5f, AimDirection.z), Vector3.up));        
        PlayShootSound(spreadShotSFX);

    }

    private void FireLaser()
    {
        Vector3 AimDirection = (mouseWorldPosition - spawnPoint.position).normalized;
        laserObject.GetComponent<LaserHandler>().ToggleLaser(true, AimDirection);
        //Instantiate(laserProjPrefab, spawnPoint.position, Quaternion.LookRotation(AimDirection, Vector3.up));      
        PlayShootSound(laserShotSFX);

    }
}
