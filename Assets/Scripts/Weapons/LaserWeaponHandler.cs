using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserWeaponHandler : MonoBehaviour
{
    [SerializeField] private float gunRange = 50f;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private float laserDuration = 0.05f;
    [SerializeField] private IntReference _damageAmount;
    bool canFire = true;
    LineRenderer laserLine;
    float fireTimer;
    public LayerMask layer;

    private List<EntityHealth> DamagedDuringray = new List<EntityHealth>();
    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        laserLine.SetPosition(0, transform.position);
        RaycastHit hit;
        if (laserLine.enabled)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 99999f, ~LayerMask.GetMask("OnlyBlockPlayer")))
            {
                laserLine.SetPosition(1, hit.point);
                EntityHealth healthObj = hit.collider.gameObject.GetComponent<EntityHealth>();
                if (healthObj != null && !DamagedDuringray.Contains(healthObj))
                {
                    healthObj.DamageEntity(_damageAmount.Value);
                    DamagedDuringray.Add(healthObj);
                }
            }
        }

    }

    public void TryShootLaser()
    {
        if (canFire)
        {
            StartCoroutine(ShootLaser());
        }
        else
        {
            Debug.Log("Laser Recharging");
        }
    }

    IEnumerator ShootLaser()
    {
        DamagedDuringray.Clear();
        canFire = false;
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
}
