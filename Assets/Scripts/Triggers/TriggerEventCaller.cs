using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventCaller : MonoBehaviour
{
    [SerializeField] UnityEvent OnColliderTrigger;

    [SerializeField] private int _triggerLayer;

    private void OnTriggerEnter(Collider other)
    {
        bool isObject = other.gameObject.layer == _triggerLayer;
        if (isObject)
        {
            OnColliderTrigger.Invoke();
        }
        
    }
}
