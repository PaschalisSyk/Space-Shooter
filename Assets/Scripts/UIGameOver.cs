using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    [SerializeField] TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    void Start()
    {
        scoreText.text = ("You Scored:\n " + scoreKeeper.GetScore().ToString("000000000"));
    }

}
