using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierBurstAttack : MonoBehaviour
{
    private int contactDamage = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<BaseEnemyController>().canAttack = false;
            other.gameObject.GetComponent<EntityHealth>().DamageEntity(contactDamage); 
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
