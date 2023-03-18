using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIGameOver : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    [SerializeField] TextMeshProUGUI scoreText;
    public Button button;
    LevelManager levelManager;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }
    void Start()
    {
        GetComponent<CanvasGroup>().DOFade(1, 1);
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(Restart);
        scoreText.text = ("You Scored:\n " + scoreKeeper.GetScore().ToString("00000"));
    }
    
    void Restart()
    {
        levelManager.LoadGame();
        levelManager.levelCount = 0;
    }

}
