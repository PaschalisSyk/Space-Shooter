using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SpaceshipSelector : MonoBehaviour
{
    public GameObject[] spaceshipPanels;
    public TextMeshProUGUI[] spaceshipNames;
    public int currentSpaceshipIndex = 0;
    public Color selectionColor;
    //public Button selectButton;
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
        SelectAnim();

        PlayerPrefs.SetInt("SelectedSpaceship", currentSpaceshipIndex);
        PlayerPrefs.Save();


        //if (!spaceshipLocked[currentSpaceshipIndex])
        //{

        //load the game or move to the next screen
        //}

        //if (currentSpaceshipIndex == 0)
        //{
        //    spaceshipPanels[0].gameObject.SetActive(true);
        //}
        //else spaceshipPanels[1].gameObject.SetActive(true);
    }

    void SelectAnim()
    {
        //selectButton.transform.GetChild(0).gameObject.SetActive(false);
        //selectButton.GetComponent<Image>().DOFade(0, 0.5f);
        spaceshipNames[currentSpaceshipIndex].DOColor(selectionColor, 0.5f).OnComplete(() => spaceshipNames[currentSpaceshipIndex].DOFade(0, 1));
        spaceshipPanels[currentSpaceshipIndex].transform.DOScale(1f, 1f).SetLoops(2 , LoopType.Yoyo).SetEase(Ease.InSine);

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
