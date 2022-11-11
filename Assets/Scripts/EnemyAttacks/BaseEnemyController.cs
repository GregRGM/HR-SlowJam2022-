using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyController : MonoBehaviour
{
    public BaseAttack _attackType;

    public float _AttackDelay;
    [HideInInspector] public float time;
    [HideInInspector] public bool canAttack;

    public virtual void Attack()
    {

    }
}

public enum TargetingType
{
  FORWARD, ATPLAYER, RANDOM
}
