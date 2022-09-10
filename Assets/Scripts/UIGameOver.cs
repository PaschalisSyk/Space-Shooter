using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(Restart);
        scoreText.text = ("You Scored:\n " + scoreKeeper.GetScore().ToString("000000000"));
    }
    
    void Restart()
    {
        levelManager.LoadGame();
        levelManager.levelCount = 0;
    }

}
