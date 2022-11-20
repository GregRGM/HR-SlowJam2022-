using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        bool isPlayer = other.gameObject.layer == 6;
    }

    public void EnterEndMode()
    {
        SceneManager.LoadScene(2);
    }
}
