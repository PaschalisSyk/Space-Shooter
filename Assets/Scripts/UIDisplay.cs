using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    [SerializeField] Health health;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Slider slider;
    float maxHealth = 50;


    private void Awake()
    {
        health = FindObjectOfType<Health>();
    }
    private void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        slider.value = maxHealth;
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
}
