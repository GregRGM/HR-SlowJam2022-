using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class BaseProjectile : MonoBehaviour
{
    private Rigidbody _proRB;
    [SerializeField] private float _pSpeed;
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private float _meshDelay;

    [SerializeField] private IntReference _damageAmount;
    private void Awake()
    {
        _proRB = GetComponent<Rigidbody>();
        if(renderer == null)
            renderer = GetComponent<MeshRenderer>();

        renderer.enabled = false;
    }

    IEnumerator Start()
    {
        _proRB.velocity = transform.forward * _pSpeed;
        yield return new WaitForSeconds(_meshDelay);
        renderer.enabled = true;
        Destroy(gameObject, 6f);
    }

    private void OnTriggerEnter(Collider other)
    {
        EntityHealth healthobj = other.GetComponent<EntityHealth>();
        if (healthobj != null)
        {
            healthobj.DamageEntity(10);
            Destroy(gameObject);
        }
    }
}
