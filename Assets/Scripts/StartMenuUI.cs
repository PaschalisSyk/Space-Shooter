using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartMenuUI : MonoBehaviour
{
    LevelManager levelManager;
    SpaceshipSelector spaceshipSelector;
    [SerializeField] Transform startPanel;
    [SerializeField] GameObject[] spaceShip;
    [SerializeField] Image image;
    [SerializeField] GameObject spaceshipPanel;

    private void Awake()
    {
        spaceshipSelector = FindObjectOfType<SpaceshipSelector>();
        levelManager = FindObjectOfType<LevelManager>();
        image.DOFade(0, 4f).SetEase(Ease.InOutSine);
    }

    public void PlayButton()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        //startPanel.transform.DOScaleY(0, 1f).SetEase(Ease.Flash).SetAutoKill(true);
        spaceshipPanel.transform.DOScaleY(0, 0.5f).SetEase(Ease.Flash).SetAutoKill(true);
        yield return new WaitForSeconds(1);
        StartAnim();
        yield return new WaitForSeconds(3);
        levelManager.LoadGame();
    }

    void StartAnim()
    {
        spaceShip[spaceshipSelector.currentSpaceshipIndex].transform.DOMoveY(50, 3).SetEase(Ease.InFlash).SetAutoKill(true);
    }

    public void NewGameButton()
    {
        startPanel.gameObject.SetActive(false);
        spaceshipPanel.SetActive(true);
    }
}
