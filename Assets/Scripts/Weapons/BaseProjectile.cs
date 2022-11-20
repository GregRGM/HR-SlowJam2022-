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
    [SerializeField] private LayerMask _nocollide;
    [SerializeField] private float _trackbackDistance;

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

    private void Update()
    {
        if (_renderer.enabled == true)
        {
            TrackBackCollision();
        }
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

    private void TrackBackCollision()
    {
        bool hitObj = Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, _trackbackDistance, ~_nocollide);

        if (hitObj)
        {
            GameObject feedback = GameObject.Instantiate(hitFeedback, transform.position, transform.rotation);
            Debug.Log("Hit object");
            bool HitEnemyWithPlayerProjectile = raycastHit.collider.gameObject.layer == 9 && gameObject.layer == 7;
            bool HitPlayerWithEnemyProjectile = raycastHit.collider.gameObject.layer == 6 && gameObject.layer == 10;

            if (HitEnemyWithPlayerProjectile || HitEnemyWithPlayerProjectile)
            {
                EntityHealth healthobj = raycastHit.collider.GetComponent<EntityHealth>();
                if (healthobj != null)
                {
                    healthobj.DamageEntity(_damageAmount.ConstantValue);
                    GameObject.Instantiate(damageFeedback, transform.position, transform.rotation);
                }
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
