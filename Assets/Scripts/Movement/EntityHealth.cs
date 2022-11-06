using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private IntReference _currentHealth;
    //we can move this to a int scriptable object if needed
    private int _maxHealth;

    public void DamageEntity(int amount)
    {
        if (!WillKillEntity(amount))
        {
            _currentHealth.Variable.RuntimeValue -= amount;
        }
        else
        {
            KillEntity();
        }
    }
    public void HealEntity(int amount)
    {
        bool willOverheal = (_currentHealth.Variable.RuntimeValue + amount > _maxHealth);
        if (willOverheal)
        {
            _currentHealth.Variable.RuntimeValue = _maxHealth;
        }
        else
        {
            _currentHealth.Variable.RuntimeValue += amount;
        }
    }

    private bool WillKillEntity(int amount)
    {
        int theoryhealth = _currentHealth.Variable.RuntimeValue;
        return (theoryhealth - amount > 0);
    }

    private void KillEntity()
    {
        Destroy(gameObject);
        //also add any death VFX and sound
    }
}
