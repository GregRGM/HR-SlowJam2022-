using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private IntReference _currentHealth;
    [SerializeField] private GameObject hitEffectPrefab;
<<<<<<< Updated upstream
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private FlashMaterial flashMaterial;
<<<<<<< HEAD
=======
    [SerializeField] private Image healthImage;
=======
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private FlashSprite _flashSprite;
>>>>>>> Stashed changes
>>>>>>> parent of 8b8859f (Revert "Revert "Revert "Updated and added enemies""")
    

    //we can move this to a int scriptable object if needed
    private int _maxHealth;


    private void Awake()
    {
<<<<<<< HEAD
=======
<<<<<<< Updated upstream
        _maxHealth = _currentHealth.Value;
        
>>>>>>> parent of 8b8859f (Revert "Revert "Revert "Updated and added enemies""")
        if(flashMaterial == null)
            flashMaterial = GetComponent<FlashMaterial>();
=======
        if (_flashSprite == null)
            _flashSprite = GetComponent<FlashSprite>();
>>>>>>> Stashed changes
    }

    public void HitFeedback()
    {
        if (_flashSprite != null && _spriteRenderer != null)
            _flashSprite.FlashStart();
    }
    public void HitFeedback(Transform _hitArea)
    {
        if (_flashSprite != null && _spriteRenderer != null)
            _flashSprite.FlashStart();

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
