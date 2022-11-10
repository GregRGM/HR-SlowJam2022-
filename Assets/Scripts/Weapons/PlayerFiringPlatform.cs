
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.InputSystem;

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

    [SerializeField] AudioClip fireballShotSFX, spreadShotSFX, laserShotSFX;
    AudioSource audioSource;

    [SerializeField] private RectTransform _reticleTran;

    public bool useObjectpooling;
    private float time = 0f;

    public RectTransform parent;

    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        mouseWorldPosition = Vector3.zero;
        time += Time.deltaTime;
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, AimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
            hitpoint.position = raycastHit.point;
            Vector3 screenPosition = _mainCamera.WorldToScreenPoint(raycastHit.point);
            _reticleTran.localPosition = (screenPosition - parent.position);

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
            if(weaponSelection < 0)
            {
                weaponSelection = WeaponSelection.Laser;
            }
        }
        //Will switch this to input system
        if (Input.GetMouseButton(0))
        {
            switch (weaponSelection)
            {
                case WeaponSelection.Fireball:
                    {
                        if (time > fireballShotRate)
                        {
                            time = 0f;
                            FireShot();
                        }
                    }
                    break;
                case WeaponSelection.Spread:
                    {
                        if (time > spreadShotRate)
                        {
                            time = 0f;
                            FireSpread();
                        }
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
    }
    private void PlayShootSound(AudioClip _audio)
    {
        AudioSource.PlayClipAtPoint(_audio, transform.position);
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
            PlayShootSound(fireballShotSFX);
        }
        else
        {
            Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(AimDirection, Vector3.up));
            PlayShootSound(fireballShotSFX);
        }
    }
    //This needs some work
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
            projectileRight.transform.rotation = Quaternion.LookRotation(new Vector3(AimDirection.x * 1.3f, AimDirection.y * 1.3f, AimDirection.z), Vector3.up);
            projectileRight.SetActive(true);
            GameObject projectileLeft = ProjectilePoolManager.instance.GetPooledSingleProjectileObj();
            projectileLeft.transform.position = spawnPoint.position;
            projectileLeft.transform.rotation = Quaternion.LookRotation(new Vector3(AimDirection.x * 1.3f, AimDirection.y * 1.3f, AimDirection.z), Vector3.up);
            projectileLeft.SetActive(true);
            PlayShootSound(fireballShotSFX);
        }
        else
        {
            Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(AimDirection, Vector3.up));
            Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(new Vector3(AimDirection.x * 1.1f, AimDirection.y * 1.1f, AimDirection.z), Vector3.up));
            Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(new Vector3(AimDirection.x * .9f, AimDirection.y * .9f, AimDirection.z), Vector3.up));
            PlayShootSound(fireballShotSFX);
        }
    }
    private void FireLaser()
    {
        Vector3 AimDirection = (mouseWorldPosition - spawnPoint.position).normalized;
        laserObject.GetComponent<LaserHandler>().ToggleLaser(true, AimDirection);
        //Instantiate(laserProjPrefab, spawnPoint.position, Quaternion.LookRotation(AimDirection, Vector3.up));      
        PlayShootSound(laserShotSFX);
    }
    private void FireTimedLaser()
    {
        Vector3 AimDirection = (mouseWorldPosition - spawnPoint.position).normalized;
        _laserHandler.gameObject.transform.rotation = Quaternion.LookRotation(AimDirection, Vector3.up);
        _laserHandler.TryShootLaser();
        PlayShootSound(laserShotSFX);
    }
}
enum WeaponSelection
{
    Fireball,
    Spread,
    Laser
}