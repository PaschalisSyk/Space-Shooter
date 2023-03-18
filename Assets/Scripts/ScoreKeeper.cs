using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int score;
    public int previousScore;
    public bool updateOn = false;
    //UIDisplay uIDisplay;

    static ScoreKeeper instance;


    private void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void ModifyScore(int value)
    {
        previousScore = score;
        score += value;
        //uIDisplay.UpdateScore();
        updateOn = true;
        Mathf.Clamp(score, 0, int.MaxValue);
        print(score);
    }

    public void ResetScore()
    {
        score = 0;
    }

}
