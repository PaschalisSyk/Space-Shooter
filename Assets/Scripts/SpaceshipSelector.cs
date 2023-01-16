using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SpaceshipSelector : MonoBehaviour
{
    public GameObject[] spaceshipPanels;
    public int currentSpaceshipIndex = 0;
    //public Button nextButton;
    //public Button prevButton;
    //public Button selectButton;
    //public Text spaceshipNameText;
    //public Image spaceshipImage;
    //public Image lockImage;
    //public Color lockedColor;
    //public Color unlockedColor;
    //public bool[] spaceshipLocked;
    //public int[] unlockRequirements;

    void Start()
    {
        //UpdateUI();
    }

    public void NextSpaceship()
    {
        currentSpaceshipIndex++;
        if (currentSpaceshipIndex >= spaceshipPanels.Length)
        {
            currentSpaceshipIndex = spaceshipPanels.Length;
        }
        //UpdateUI();
    }

    public void PrevSpaceship()
    {
        currentSpaceshipIndex--;
        if (currentSpaceshipIndex < 0)
        {
            currentSpaceshipIndex = 0;
        }
        //UpdateUI();
    }

    public void SelectSpaceship()
    {
        //if (!spaceshipLocked[currentSpaceshipIndex])
        //{
            PlayerPrefs.SetInt("SelectedSpaceship", currentSpaceshipIndex);
            PlayerPrefs.Save();
        //load the game or move to the next screen
        //}

        //if (currentSpaceshipIndex == 0)
        //{
        //    spaceshipPanels[0].gameObject.SetActive(true);
        //}
        //else spaceshipPanels[1].gameObject.SetActive(true);
    }

    //public void UpdateUI()
    //{
    //    for (int i = 0; i < spaceshipPanels.Length; i++)
    //    {
    //        spaceshipPanels[i].SetActive(i == currentSpaceshipIndex);
    //    }

    //    spaceshipNameText.text = spaceshipPanels[currentSpaceshipIndex].name;
    //    spaceshipImage.sprite = spaceshipPanels[currentSpaceshipIndex].GetComponent<Image>().sprite;
    //    lockImage.gameObject.SetActive(spaceshipLocked[currentSpaceshipIndex]);
    //    lockImage.color = spaceshipLocked[currentSpaceshipIndex] ? lockedColor : unlockedColor;
    //    selectButton.interactable = !spaceshipLocked[currentSpaceshipIndex];
    //}
}
