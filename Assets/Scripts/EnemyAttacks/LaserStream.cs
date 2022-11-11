using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserStream : BaseAttack
{

    [SerializeField] private Transform _originPos;
    [SerializeField] private Transform _hitPosition;
    [Tooltip("Larger the number slower the laser travels")]
    [SerializeField] private float _laserDelay;
    [SerializeField] private float _laserDuration;
    [SerializeField] private LineRenderer _renderer;
    [SerializeField] private LineRenderer _warningRenderer;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _CollisionLayer;
    [SerializeField] private int _damagePerCollision;
    [SerializeField] private float _radius;

    [SerializeField] private float _rechargeRate;

    private List<EntityHealth> DamagedDuringray = new List<EntityHealth>();
    private float time = 0f;

    private void OnEnable()
    {
        _renderer.enabled = false;
        _warningRenderer.enabled = false;
    }

    public override void FireAttack()
    {
        if (time > _rechargeRate)
        {
            time = 0f;
            StartLaser();
        }
    }

    [ContextMenu("Start Laser")]
    public void StartLaser()
    {
        StartCoroutine(StartLaserDelay());
        DamagedDuringray.Clear();
    }
    public void StopLaser()
    {
        Debug.Log("stopped Laser");
        _renderer.enabled = false;
    }
    private void Update()
    {
        if (_renderer.enabled)
        {
            SphereCheck();
            UpdateLineRenderer();
        }
        else if (!_renderer.enabled && !_warningRenderer.enabled)
        {
            time += Time.deltaTime;
        }
        
    }
    private void SphereCheck()
    {
        //Vector3 AimDirection = (_hitPosition.position - _originPos.position).normalized;
       // Quaternion Directiontowards = Quaternion.LookRotation(AimDirection, Vector3.up);
        RaycastHit hit;
        bool DidHitPlayer = Physics.SphereCast(_originPos.position, _radius, _originPos.forward, out hit, 999f, _playerLayer);
        if (DidHitPlayer)
        {
            Debug.Log("Hit Player");
            EntityHealth healthObj = hit.collider.gameObject.GetComponent<EntityHealth>();
            if (healthObj != null && !DamagedDuringray.Contains(healthObj))
            {
                healthObj.DamageEntity(_damagePerCollision);
                DamagedDuringray.Add(healthObj);
            }
        }
    }
    private void UpdateLineRenderer()
    {
        RaycastHit hit;
        bool DidSomething = Physics.Raycast(_originPos.position, _originPos.forward, out hit, 999f, _CollisionLayer);
        if (DidSomething)
        {
            _hitPosition.position = hit.point;
        }
        _renderer.SetPosition(1, _hitPosition.position);
    }
    IEnumerator StartLaserDelay()
    {
        _warningRenderer.SetPosition(0, _originPos.position);
        _warningRenderer.SetPosition(1, _originPos.forward * 999f);
        _warningRenderer.enabled = true;
        _renderer.SetPosition(0, _originPos.position);
        yield return new WaitForSeconds(_laserDelay);
        _warningRenderer.enabled = false;
        UpdateLineRenderer();
        _renderer.enabled = true;
        yield return new WaitForSeconds(_laserDuration);
        StopLaser();
    }
}
