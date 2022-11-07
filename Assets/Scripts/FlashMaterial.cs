using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Renderer))]

//Simple Component to easily flash a material's color. Made to be modular.

public class FlashMaterial : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Color originalColor;
    [SerializeField] Color flashColor = Color.red;
    [SerializeField] float flashTime = .15f;
    [SerializeField] int flashAmount = 1; 
    
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
    }

    public void FlashStart()
    {
        if(flashAmount == 1)
            ToggleColorOn();
    }

    void ToggleColorOn()
    {
        if (meshRenderer.material.color == originalColor)
        {
            meshRenderer.material.color = flashColor;
            Invoke("ToggleColorOff", flashTime);
        }
    }
    
    void ToggleColorOff()
    {
        if (meshRenderer.material.color != originalColor)
            meshRenderer.material.color = originalColor;
    }
}
