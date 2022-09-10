using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIDisplay : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    [SerializeField] Health health;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Slider slider;
    [SerializeField] CanvasGroup endLevelPanel;
    [SerializeField] RectTransform levelComplete;
    [SerializeField] Image image;
    float maxHealth = 50;


    private void Awake()
    {
        health = FindObjectOfType<Health>();
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

        endLevelPanel.DOFade(1, 2);
        levelComplete.DOAnchorPosY(90, 2);
        Invoke("FadeOut",3);
    }

    void FadeIn()
    {
        image.DOFade(0, 2);
    }

    void FadeOut()
    {
        image.DOFade(1, 2);
    }
}
