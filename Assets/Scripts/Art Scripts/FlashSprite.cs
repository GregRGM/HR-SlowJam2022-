using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSprite : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    Color originalColor;
    [SerializeField] Color flashColor = Color.red;
    [SerializeField] float flashTime = .15f;
    [SerializeField] int flashAmount = 1;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = _spriteRenderer.color;
    }

    public void FlashStart()
    {
        if (flashAmount == 1)
            ToggleColorOn();
    }

    void ToggleColorOn()
    {
        if (_spriteRenderer.color == originalColor)
        {
            _spriteRenderer.color = flashColor;
            Invoke("ToggleColorOff", flashTime);
        }
    }

    void ToggleColorOff()
    {
        if (_spriteRenderer.color != originalColor)
            _spriteRenderer.color = originalColor;
    }
}
