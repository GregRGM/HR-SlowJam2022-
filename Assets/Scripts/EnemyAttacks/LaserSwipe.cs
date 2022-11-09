using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwipe : MonoBehaviour
{
    [SerializeField] private Transform _originPos;
    [SerializeField] private Transform _hitPosition;
    [SerializeField] private Transform[] _endPoints;
    [Tooltip("Larger the number slower the laser travels")]
    [SerializeField] private float _laserDelay;
    [SerializeField] private LineRenderer _renderer;
    [SerializeField] private int _laserIndex;
    private bool _shouldCast;
    [SerializeField] private float _positionIsh;

    [SerializeField] private LayerMask _playerLayer;

    [SerializeField] private int _damagePerCollision;

    private List<EntityHealth> DamagedDuringray = new List<EntityHealth>();

    public GameObject Debugpoint;

    private float timePast;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        StartLaser();
    }

    [ContextMenu("Start Laser")]
    public void StartLaser()
    {
        _laserIndex = 0;
        //_hitPosition = _endPoints[0];
        _renderer.enabled = true;
        _shouldCast = true;
        _renderer.SetPosition(0, _originPos.position);
        _hitPosition.position = _originPos.position;
        DamagedDuringray.Clear();
    }
    public void StopLaser()
    {
        Debug.Log("stopped Laser");
        _renderer.enabled = false;
        _shouldCast = false;
        _laserIndex = 0;
        _hitPosition.position = _originPos.position;
    }
    //work on smoothing out the lerping
    private void Update()
    {
        if (_shouldCast)
        {
            bool isCloseEnough = Vector3.Distance(_hitPosition.position, _endPoints[_laserIndex].position) < _positionIsh;
            if (isCloseEnough)
            {
                if (_laserIndex != (_endPoints.Length-1))
                {
                    Debug.Log("Made it close enough");
                    _laserIndex++;
                    timePast = 0f;

                }
                else
                {
                    Debug.Log("Should stop Laser");
                    StopLaser();
                    return;
                }
            }
            LerpToNextPoint();
            SphereCheck();
            UpdateLineRenderer();
        }
    }

    private void LerpToNextPoint()
    {
        if (_laserIndex <= _endPoints.Length)
        {
            timePast += Time.deltaTime;
            float ratioComplete = timePast / _laserDelay;
            _hitPosition.position = Vector3.Lerp(_hitPosition.position, _endPoints[_laserIndex].position, ratioComplete);
        }    
    }

    private void SphereCheck()
    {
       
        Vector3 AimDirection = (_hitPosition.position - _originPos.position).normalized;
        Quaternion Directiontowards = Quaternion.LookRotation(AimDirection, Vector3.up);
        RaycastHit hit;
        //bool DidHit = Physics.Raycast(_originPos.position, AimDirection, out hit,999f, LayerMask.GetMask("Ground"));
        //if (DidHit)
        //{
        //    Debugpoint.transform.position = hit.point;
        //}
        //Need to make this a spherecast
        bool DidHitPlayer = Physics.Raycast(_originPos.position, AimDirection, out hit,999f, _playerLayer);
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
        _renderer.SetPosition(1, _hitPosition.position);
    }
}
