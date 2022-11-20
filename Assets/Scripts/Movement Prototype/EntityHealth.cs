using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private IntReference _currentHealth;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private Image healthImage;
    [SerializeField] private int _healthSlices;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private FlashSprite _flashSprite;
    

    //we can move this to a int scriptable object if needed
    private int _maxHealth;


    private void Awake()
    {
        _maxHealth = _currentHealth.Value;
        if (_flashSprite == null)
            _flashSprite = GetComponent<FlashSprite>();
    }

    private void Update()
    {
        if (healthImage != null)
        {
            int health = _currentHealth.Value;
            float healthProportion = ((float)health / _maxHealth) * _healthSlices;
            float barsToFill = Mathf.Ceil(healthProportion);

            Debug.Log(health);

            healthImage.fillAmount = barsToFill / _healthSlices;
        }
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
