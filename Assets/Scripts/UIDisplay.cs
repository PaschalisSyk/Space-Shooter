using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIDisplay : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    [SerializeField] Health health;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Slider slider;
    [SerializeField] CanvasGroup endLevelSign;
    [SerializeField] RectTransform levelComplete;
    [SerializeField] Image image;
    [SerializeField] CanvasGroup endLevelPanel;
    float maxHealth = 50;
    bool isOn;
    Player player;


    private void Awake()
    {
        health = FindObjectOfType<Health>();
        levelManager = FindObjectOfType<LevelManager>();
        player = FindObjectOfType<Player>();
        endLevelPanel.gameObject.SetActive(false);
        endLevelSign.gameObject.SetActive(false);
    }
    private void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        slider.value = maxHealth;
        FadeIn();
    }
    // Update is called once per frame
    void Update()
    {
        LerpHealth();
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
    }

    void LerpHealth()
    {
        float startingHealth = slider.value;
        float targetHealth = health.GetHealth();


        slider.value = Mathf.Lerp(slider.value, targetHealth / maxHealth, 3f * Time.deltaTime);
    }

    public void EndLevelUI()
    {
        endLevelSign.gameObject.SetActive(true);
        endLevelSign.DOFade(1, 2f).SetLink(endLevelPanel.gameObject);
        levelComplete.DOAnchorPosY(400, 2).SetLink(gameObject);
        StartCoroutine(EndLevelPaneActivation());
    }

    void FadeIn()
    {
        if(image !=null)
           image.DOFade(0, 2).SetLink(image.gameObject).SetEase(Ease.InOutSine).SetAutoKill(true);
    }

    void FadeOut()
    {
        if (image != null)
            image.DOFade(1, 2).SetLink(image.gameObject).SetAutoKill(true);
    }

    IEnumerator EndLevelPaneActivation()
    {
        yield return new WaitForSeconds(2.5f);
        endLevelPanel.gameObject.SetActive(true);
        isOn = true;
        endLevelPanel.DOFade(1, 4).SetLink(gameObject);
    }

    void FadeEndPanel()
    {
        StartCoroutine(EndPanelFade());
    }

    IEnumerator EndPanelFade()
    {
        endLevelPanel.DOFade(0, 1).SetAutoKill(true).SetLink(gameObject);
        FadeOut();
        yield return new WaitForSeconds(2);
        levelManager.LoadNextLevel();
    }

    public bool IsOn()
    {
        return isOn;
    }

}
