using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPatternEnemy : BaseEnemyController
{
    [SerializeField] private TargetingType _targetingStyle;
    private GameObject _player;
    [SerializeField] private Transform _WeaponOrigin;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private List<Transform> _movementPattern = new List<Transform>();

    public int _patternIndex;
    public float howclose;
    private float timePast;
    private bool _Increasing = true;

    private void OnEnable()
    {
        _player = FindObjectOfType<PlayerFiringPlatform>().gameObject;
    }

    private void Update()
    {
        time += Time.deltaTime;
        bool timeToAttack = time >= _AttackDelay;
        bool isCloseEnough = Vector3.Distance(transform.position, _movementPattern[_patternIndex].position) < howclose;
        if (isCloseEnough)
        {
            GetNextPoint();
        }
        LerpToNextPoint();

        if (timeToAttack)
        {
            Attack();
        }

    }

    public override void Attack()
    {
        time = 0f;
        if (_targetingStyle == TargetingType.ATPLAYER)
        {
            Vector3 AimDirection = (_player.transform.position - _WeaponOrigin.position).normalized;
            Quaternion Directiontowards = Quaternion.LookRotation(AimDirection, Vector3.up);
            _WeaponOrigin.rotation = Directiontowards;
            _attackType.FireAttack();
        }
        else
        {
            _attackType.FireAttack();
        }
    }

    private void LerpToNextPoint()
    {
        if (_patternIndex <= _movementPattern.Count -1 && _patternIndex >=0)
        {
            timePast += Time.deltaTime;
            float ratioComplete = timePast / _moveSpeed;
            transform.position = Vector3.Lerp(transform.position, _movementPattern[_patternIndex].position, ratioComplete);
        }
    }

    private void GetNextPoint()
    {
        if (_patternIndex < _movementPattern.Count-1 && _Increasing)
        {
            _patternIndex++;
            return;
        }
        else if (_patternIndex >= _movementPattern.Count-1)
        {
            _patternIndex--;
            _Increasing = false;
            timePast = 0;
            return;
            //return _movementPattern[_patternIndex].transform;
        }
        else if (!_Increasing && _patternIndex <= 0)
        {
            _Increasing = true;
            _patternIndex++;
            timePast = 0;
            return;
            //return _movementPattern[_patternIndex].transform;
        }
        else
        {
            _patternIndex--;
            return;
            //return _movementPattern[_patternIndex].transform;
        }
    }
}
