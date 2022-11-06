using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class BaseProjectile : MonoBehaviour
{
    private Rigidbody _proRB;
    [SerializeField] private float _pSpeed;
    private MeshRenderer renderer;
    [SerializeField] private float _meshDelay;

    [SerializeField] private IntReference _damageAmount;
    private void Awake()
    {
        _proRB = GetComponent<Rigidbody>();
        renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;
    }

    IEnumerator Start()
    {
        _proRB.velocity = transform.forward * _pSpeed;
        yield return new WaitForSeconds(_meshDelay);
        renderer.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        EntityHealth healthobj = other.GetComponent<EntityHealth>();
        if (healthobj)
        {
            healthobj.DamageEntity(_damageAmount.Variable.RuntimeValue);
        }
    }
}
