//using Ludiq.PeekCore;
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
    [SerializeField] private LaserWeaponHandler _laserHandler;
    [SerializeField] private Transform spawnPoint;
    public Transform hitpoint;
    private Vector3 mouseWorldPosition;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask ignoreLayer;

    [SerializeField] private float fireballShotRate = 60, spreadShotRate = 30, laserHitRate = 1f;

    [SerializeField] private WeaponSelection weaponSelection;


    public bool useObjectpooling;
    private float time = 0f;

    private void Update()
    {
        mouseWorldPosition = Vector3.zero;
        time += Time.deltaTime;
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

        if (Input.GetMouseButton(0))
        {
            switch (weaponSelection)
            {
                case WeaponSelection.Fireball:
                    {
                        //InvokeRepeating("FireShot", 0f, 1 / fireballShotRate);
                        if (time > fireballShotRate)
                        {
                            time = 0f;
                            FireShot();
                        }
                        //FireShot();
                    }
                    break;
                case WeaponSelection.Spread:
                    {
                        if (time > spreadShotRate)
                        {
                            time = 0f;
                            FireSpread();
                        }
                        //InvokeRepeating("FireSpread", 0f, 1 / spreadShotRate);
                    }
                    break;
                case WeaponSelection.Laser:
                    {
                        //FireLaser();
                        FireTimedLaser();
                    }
                    break;
                default:
                    break;
            }
        }
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    switch (weaponSelection)
        //    {
        //        case WeaponSelection.Fireball:
        //            {
        //                CancelInvoke("FireShot");
        //            }
        //            break;
        //        case WeaponSelection.Spread:
        //            {
        //                CancelInvoke("FireSpread");
        //            }
        //            break;
        //        case WeaponSelection.Laser:
        //            {
        //                //laserObject.GetComponent<LaserHandler>().ToggleLaser(false);
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //}

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(spawnPoint.position, hitpoint.position);
    }


    private void FireShot()
    {
        Vector3 AimDirection = (mouseWorldPosition - spawnPoint.position).normalized;
        if (useObjectpooling)
        {
            GameObject projectile = ProjectilePoolManager.instance.GetPooledSingleProjectileObj();
            projectile.transform.position = spawnPoint.position; //this works
            projectile.transform.rotation = Quaternion.LookRotation(AimDirection, Vector3.up); // this doesn't
            Debug.Log(AimDirection);
        }
        else
        {
            Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(AimDirection, Vector3.up));
        }
    }
    private void FireSpread()
    {
        Vector3 AimDirection = (mouseWorldPosition - spawnPoint.position).normalized;
        if (useObjectpooling)
        {
            //revisit angled stuff not important right now sorta works kinda got alot of junk in the trunk - SG
            GameObject projectileCenter = ProjectilePoolManager.instance.GetPooledSingleProjectileObj();
            projectileCenter.transform.position = spawnPoint.position;
            projectileCenter.transform.rotation = Quaternion.LookRotation(AimDirection, Vector3.up);
            projectileCenter.SetActive(true);
            GameObject projectileRight = ProjectilePoolManager.instance.GetPooledSingleProjectileObj();
            projectileRight.transform.position = spawnPoint.position;
            projectileRight.transform.rotation = Quaternion.LookRotation(new Vector3(AimDirection.x * 1.1f, AimDirection.y * 1.1f, AimDirection.z), Vector3.up);
            projectileRight.SetActive(true);
            GameObject projectileLeft = ProjectilePoolManager.instance.GetPooledSingleProjectileObj();
            projectileLeft.transform.position = spawnPoint.position;
            projectileLeft.transform.rotation = Quaternion.LookRotation(new Vector3(AimDirection.x * 1.1f, AimDirection.y * 1.1f, AimDirection.z), Vector3.up);
            projectileLeft.SetActive(true);
        }
        else
        {
            Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(AimDirection, Vector3.up));
            Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(new Vector3(AimDirection.x * 1.1f, AimDirection.y * 1.1f, AimDirection.z), Vector3.up));
            Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(new Vector3(AimDirection.x * .9f, AimDirection.y * .9f, AimDirection.z), Vector3.up));
        }
    }
    private void FireLaser()
    {
        Vector3 AimDirection = (mouseWorldPosition - spawnPoint.position).normalized;
        laserObject.GetComponent<LaserHandler>().ToggleLaser(true, AimDirection);
        //Instantiate(laserProjPrefab, spawnPoint.position, Quaternion.LookRotation(AimDirection, Vector3.up));      

    }
    private void FireTimedLaser()
    {
        Vector3 AimDirection = (mouseWorldPosition - spawnPoint.position).normalized;
        _laserHandler.gameObject.transform.rotation = Quaternion.LookRotation(AimDirection, Vector3.up);
        _laserHandler.TryShootLaser();
    }
}
