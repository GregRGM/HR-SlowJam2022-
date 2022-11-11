using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemy : BaseEnemyController
{
    [SerializeField] private TargetingType _targetingStyle;
    private GameObject _player;
    [SerializeField] private Transform _WeaponOrigin;

    private void OnEnable()
    {
        _player = FindObjectOfType<PlayerFiringPlatform>().gameObject;
    }

    private void Update()
    {
        time += Time.deltaTime;
        bool timeToAttack = time >= _AttackDelay;
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
}
