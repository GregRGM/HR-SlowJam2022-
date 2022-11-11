using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// yeah this is based off Brackeys code
public enum CombatState {
    COMBAT, // Fighting enemies
    NAVIGATION, // Navigating to fight enemies
}

public class CombatToNavigationHandler : MonoBehaviour
{
    public CombatState combatState = CombatState.COMBAT;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
