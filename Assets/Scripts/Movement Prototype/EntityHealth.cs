using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private IntReference _currentHealth;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private FlashMaterial flashMaterial;
    

    //we can move this to a int scriptable object if needed
    private int _maxHealth;


    private void Awake()
    {
        if(flashMaterial == null)
            flashMaterial = GetComponent<FlashMaterial>();
    }

    public void HitFeedback()
    {
        if (flashMaterial != null && meshRenderer != null)
            flashMaterial.FlashStart();

    }
    public void HitFeedback(Transform _hitArea)
    {
        if (flashMaterial != null && meshRenderer != null)
            flashMaterial.FlashStart();

    }

    public void DamageEntity(int amount)
    {
        HitFeedback();
        
        if (!WillKillEntity(amount))
        {
            int health = _currentHealth.Value;
            health -= amount;

            Debug.Log(gameObject.name + "took " + amount + " damage!");
            _currentHealth.SetValue(health);
        }
        else
        {
            Debug.Log("Killed enemy");
            KillEntity();
        }
    }
    public void HealEntity(int amount)
    {
        bool willOverheal = (_currentHealth.Variable.RuntimeValue + amount > _maxHealth);
        if (willOverheal)
        {
            _currentHealth.SetValue(_maxHealth);
        }
        else
        {
            int health = _currentHealth.Value;
            health += amount;
            _currentHealth.SetValue(health);
        }
    }



    private bool WillKillEntity(int amount)
    {
        int theoryhealth = _currentHealth.Value;
        return ((theoryhealth - amount) < 0.1f);
    }

    private void KillEntity()
    {
        Destroy(gameObject);
        //also add any death VFX and sound
    }
}
