using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//keeping this one
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class BaseProjectile : MonoBehaviour
{
    private Rigidbody _proRB;
    [SerializeField] private float _pSpeed, _lifeTime, _meshDelay;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private bool _isPooled;
    [SerializeField] private IntReference _damageAmount;
    [SerializeField] private GameObject hitFeedback, damageFeedback;

    private void Awake()
    {
        _proRB = GetComponent<Rigidbody>();
        if(_renderer == null)
            _renderer = GetComponent<MeshRenderer>();

        _renderer.enabled = false;
    }

    private void OnEnable()
    {
        StartCoroutine(FireCo());
    }

    IEnumerator FireCo()
    {
        yield return new WaitForSeconds(0.1f);
        _proRB.velocity = transform.forward * _pSpeed;
        yield return new WaitForSeconds(_meshDelay);
        _renderer.enabled = true;
        yield return new WaitForSeconds(_lifeTime);
        if (_isPooled)
        {
            //Debug.Log("I stopped bitch");
            _proRB.velocity = Vector3.zero;
            StopAllCoroutines();
            ProjectilePoolManager.instance.ReturnPlayerSingleProjectile(this.gameObject);
            //gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject hitObj = GameObject.Instantiate(hitFeedback, transform.position, transform.rotation);
        if (other.gameObject.layer == 3)
        {
            if (_isPooled)
            {
                ReturnPooledObjects();
                return;
            }
        }
        // this is Ugly but its my ugly
        bool HitEnemyWithPlayerProjectile = other.gameObject.layer == 9 && gameObject.layer == 7;
        bool HitPlayerWithEnemyProjectile = other.gameObject.layer == 6 && gameObject.layer == 10;

        if (HitEnemyWithPlayerProjectile || HitEnemyWithPlayerProjectile)
        {
            EntityHealth healthobj = other.GetComponent<EntityHealth>();
            if (healthobj != null)
            {
                healthobj.DamageEntity(_damageAmount.ConstantValue);
                GameObject.Instantiate(damageFeedback, transform.position, transform.rotation);
            }
            if (_isPooled)
            {
                ReturnPooledObjects();
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }

    private void ReturnPooledObjects()
    {
        _proRB.velocity = Vector3.zero;
        StopAllCoroutines();
        if (gameObject.layer == 7)
        {
            ProjectilePoolManager.instance.ReturnPlayerSingleProjectile(this.gameObject);
        }
        if (gameObject.layer == 10)
        {
            ProjectilePoolManager.instance.ReturnEnemySingleProjectile(this.gameObject);
        }
    }
    
}
