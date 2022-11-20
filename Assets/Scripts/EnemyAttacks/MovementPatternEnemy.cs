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
    [SerializeField] private bool stopsAtEndOfPattern;

    [SerializeField] private Animator _anim;

    public int _patternIndex;
    public float howclose;
    private float timePast;
    private bool _Increasing = true;
    public float delayBetweenMovingToNextPoint = 0.0001f;

    private void OnEnable()
    {
        _player = FindObjectOfType<PlayerFiringPlatform>().gameObject;
        _patternIndex = 0;
    }

    private void Update()
    {
        time += Time.deltaTime;
        bool timeToAttack = time >= _AttackDelay;
        bool isCloseEnough = Vector3.Distance(transform.position, _movementPattern[_patternIndex].position) < howclose;
        if (isCloseEnough)
        {
            StartCoroutine(PointDelayCO());
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
        //this is why the animator is a shit system
        _anim.SetTrigger("Attacking");
    }

    private void LerpToNextPoint()
    {
        if (_patternIndex <= _movementPattern.Count -1 && _patternIndex >=0)
        {
            timePast += Time.deltaTime;
            //float ratioComplete = timePast / _moveSpeed;
            ////float ratioComplete = timePast / (1/ _moveSpeed);
            ////float ratioComplete = (timePast / (1/ _moveSpeed)) * 10;
            ////ratioComplete /= 2f;
            //Debug.Log(gameObject.name + "'s ratioComplete is currently " + ratioComplete);
            //transform.position = Vector3.Lerp(transform.position, _movementPattern[_patternIndex].position, ratioComplete);

            var step = _moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _movementPattern[_patternIndex].position, step);

        }
    }

    private void GetNextPoint()
    {
        if (stopsAtEndOfPattern && _patternIndex >= _movementPattern.Count-1)
        {
            return;
        }
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

    IEnumerator PointDelayCO()
    {
        yield return new WaitForSeconds(delayBetweenMovingToNextPoint);
        GetNextPoint();
        StopAllCoroutines();
    }

    // Assumes the caller is ensuring the gameobject is a player
    public void IntializeEnemy(List<Transform> newpattern)
    {
        _movementPattern.Clear();
        _movementPattern = newpattern;
    }

    public void TriggerHurt()
    {
        _anim.SetTrigger("Hit");
    }
}
