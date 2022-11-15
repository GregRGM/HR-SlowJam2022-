using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenshotController : MonoBehaviour
{
    public List<Sprite> ScreenShots = new List<Sprite>();
    public List<string> ScreenShotDescriptions = new List<string>();
    public List<GameObject> ImageIndex = new List<GameObject>();

    public GameObject nextbutton;
    public GameObject previousbutton;
    public Image CurrentImage;
    public TextMeshProUGUI CurrentText;
    private int index;

    public void NextImage()
    {
        if (index == ScreenShots.Count-2)
        {
            nextbutton.SetActive(false);
        }
        index++;
        UpdateUI();
        previousbutton.SetActive(true);
        
    }

    public void PreviousImage()
    {
        if (index == 1)
        {
           
            previousbutton.SetActive(false);
        }
        index--;
        UpdateUI();
        nextbutton.SetActive(true);
    }
    private void UpdateUI()
    {
        CurrentImage.sprite = ScreenShots[index];
        CurrentText.text = ScreenShotDescriptions[index];
        foreach (var item in ImageIndex)
        {
            item.SetActive(false);
        }
        for (int i = 0; i < index; i++)
        {
            ImageIndex[i].SetActive(true);
        }
    }
}
