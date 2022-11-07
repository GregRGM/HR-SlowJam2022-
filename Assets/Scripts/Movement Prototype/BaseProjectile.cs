using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class BaseProjectile : MonoBehaviour
{
    private Rigidbody _proRB;
    [SerializeField] private float _pSpeed, _lifeTime, _meshDelay;
    [SerializeField] private MeshRenderer meshRenderer;
    

    [SerializeField] private IntReference _damageAmount;

    [SerializeField] private GameObject hitFeedback;
    private void Awake()
    {
        _proRB = GetComponent<Rigidbody>();
        if(GetComponent<Renderer>() == null)
            meshRenderer = GetComponent<MeshRenderer>();

        GetComponent<Renderer>().enabled = false;
    }

    IEnumerator Start()
    {
        _proRB.velocity = transform.forward * _pSpeed;
        yield return new WaitForSeconds(_meshDelay);
        GetComponent<Renderer>().enabled = true;
        Destroy(gameObject, 6f);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject.Instantiate(hitFeedback, transform.position, transform.rotation);

        EntityHealth healthobj = other.GetComponent<EntityHealth>();
        if (healthobj != null)
        {
            healthobj.DamageEntity(_damageAmount.ConstantValue);
            Destroy(gameObject);
        }
    }
}
