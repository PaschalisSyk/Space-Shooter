﻿using System.Collections;
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
    [SerializeField] GameObject endLevelSign;
    [SerializeField] RectTransform levelComplete;
    [SerializeField] Image image;
    [SerializeField] CanvasGroup endLevelPanel;
    float maxHealth = 50;
    bool isOn;
    Player player;


    private void Awake()
    {
        //health = FindObjectOfType<Health>();
        levelManager = FindObjectOfType<LevelManager>();
        player = FindObjectOfType<Player>();
        
        endLevelPanel.gameObject.SetActive(false);
        endLevelSign.gameObject.SetActive(false);

    }
    private void Start()
    {
        //player = levelManager.GetPlayer();
        maxHealth = player.GetComponent<Health>().GetHealth();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        slider.value = maxHealth;
        FadeIn();
    }
    // Update is called once per frame
    void Update()
    {
        LerpHealth();
        //scoreText.text = scoreKeeper.GetScore().ToString("000000000");
    }

    private void FixedUpdate()
    {
        UpdateScore();
    }

    void LerpHealth()
    {
        float startingHealth = slider.value;
        float targetHealth = player.GetComponent<Health>().GetHealth();


        slider.value = Mathf.Lerp(slider.value, targetHealth / maxHealth, 3f * Time.deltaTime);
    }

    public void EndLevelUI()
    {
        //endLevelSign.gameObject.SetActive(true);
        //endLevelSign.DOFade(1, 2f).SetLink(endLevelPanel.gameObject).OnComplete(() => endLevelSign.alpha = 1)
        //    .OnComplete(() => endLevelSign.DOFade(0, 1).SetEase(Ease.OutSine));
        //levelComplete.DOAnchorPosY(840, 2).SetEase(Ease.InSine).SetLink(gameObject);
        //textMeshPro.DOFade(1, 2).OnComplete(() => textMeshPro.DOFade(0, 1));
        endLevelSign.SetActive(true);
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
        endLevelPanel.DOFade(1, 2).SetLink(gameObject);
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

    public void UpdateScore()
    {
        StartCoroutine(IncrementScore());

    }

    IEnumerator IncrementScore()
    {
        if(scoreKeeper.updateOn)
        {
            while (scoreKeeper.previousScore < scoreKeeper.GetScore())
            {
                scoreKeeper.previousScore += 1;
                scoreText.text = scoreKeeper.previousScore.ToString("000000000");
                yield return new WaitForFixedUpdate();
            }
            scoreKeeper.updateOn = false;
        }

    }

}
