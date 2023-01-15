using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    static LevelManager instance;

    [SerializeField] float sceneLoadDelay = 6;
    [SerializeField] public int levelCount = 0;
    [SerializeField] int gunIndex = 0;

    ScoreKeeper scoreKeeper;

    private void Awake()
    {
        ManageSingleton();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        LevelCounter();
    }

    public void LoadGame()
    {       
        scoreKeeper.ResetScore();
        SceneManager.LoadScene("Level1");
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

    public void LoadNextLevel()
    {         
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator WaitAndLoad(string sceneName , float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    public int LevelCount()
    {
        return levelCount;
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

    void LevelCounter()
    {
        levelCount = SceneManager.GetActiveScene().buildIndex - 1;

        if (levelCount <= 0)
        {
            levelCount = 0;
        }

    }

    public int GetGunIndex()
    {
        return gunIndex;
    }

    public void UpdateGun()
    {
        if(gunIndex >= 2)
        {
            return;
        }
        gunIndex++;
    }
}
