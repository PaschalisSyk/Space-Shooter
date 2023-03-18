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
    [SerializeField] RectTransform selectionText;
    [SerializeField] GameObject levelsPanel;

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
        //spaceshipPanel.transform.DOScaleY(0, 0.5f).SetEase(Ease.Flash).SetAutoKill(true);
        yield return new WaitForSeconds(1f);
        spaceshipPanel.GetComponent<CanvasGroup>().DOFade(0, 1f).SetEase(Ease.OutSine).SetAutoKill(true);
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
        StartCoroutine(NewGameButtonPanels());
        //startPanel.gameObject.SetActive(false);
    }

    IEnumerator NewGameButtonPanels()
    {
        startPanel.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(() => startPanel.gameObject.SetActive(false));
        spaceshipPanel.SetActive(true);
        selectionText.DOAnchorPosX(0, 1f).SetEase(Ease.OutCubic);
        yield return new WaitForSeconds(3);
        selectionText.DOAnchorPosX(-1000, 1f);
        yield return new WaitForSeconds(0.5f);
        spaceshipPanel.GetComponent<CanvasGroup>().DOFade(1, 1f);
    }

    public void LoadGameButton()
    {
        StartCoroutine(LoadButton());
    }

    IEnumerator LoadButton()
    {
        startPanel.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(() => startPanel.gameObject.SetActive(false));
        levelsPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        levelsPanel.GetComponent<CanvasGroup>().DOFade(1, 1);
    }
}
