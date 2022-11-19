using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteColorController : MonoBehaviour
{
    [SerializeField] private PlayerFiringPlatform _firingPlatform;

    [SerializeField] private SpriteRenderer _spriteR;

    [SerializeField] private Color _fireBallColor;
    [SerializeField] private Color _IcicleColor;
    [SerializeField] private Color _ShockColor;


    public void UpdateSpriteColor(WeaponSelection weapon)
    {
        if (weapon == WeaponSelection.Fireball)
        {
            _spriteR.color = _fireBallColor;
        }
        if (weapon == WeaponSelection.Spread)
        {
            _spriteR.color = _IcicleColor;
        }
        if (weapon == WeaponSelection.Laser)
        {
            _spriteR.color = _ShockColor;
        }
    }
}
