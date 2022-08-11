using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2;

    ScoreKeeper scoreKeeper;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void LoadGame()
    {
        scoreKeeper.ResetScore();
        SceneManager.LoadScene("Game");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOverMenu" , sceneLoadDelay));
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName , float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
