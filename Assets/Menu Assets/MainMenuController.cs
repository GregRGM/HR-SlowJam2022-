using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject ControlsTab;
   // public GameObject HowToPlayTab;
    public GameObject ButtonTab;
    public GameObject ExitButton;
    public GameObject CreditsPanel;
    //tbd
    public GameObject MoreGamesPanel;

    private void Start()
    {
        //HowToPlayTab.SetActive(true);
        ControlsTab.SetActive(true);
        ButtonTab.SetActive(true);
    }
    public void EnterPlayMode()
    {
        SceneManager.LoadScene(1);
    }

    public void DisplayCredits()
    {
        CreditsPanel.SetActive(true);
        ExitButton.SetActive(true);
        CloseTabs();
    }
    private void CloseTabs()
    {
       // HowToPlayTab.SetActive(false);
        ControlsTab.SetActive(false);
        ButtonTab.SetActive(false);
    }
    public void CloseTab()
    {
        CreditsPanel.SetActive(false);
        //HowToPlayTab.SetActive(true);
        ControlsTab.SetActive(true);
        ButtonTab.SetActive(true);
    }

    public void DisplayNormal()
    {
        CreditsPanel.SetActive(false);
        ExitButton.SetActive(false);
        ControlsTab.SetActive(true);
        ButtonTab.SetActive(true);
    }

    public void Quit()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
