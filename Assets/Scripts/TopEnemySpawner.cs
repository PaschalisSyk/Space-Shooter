using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopEnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Bosses
    {
        public List<GameObject> bosses;
    }
    [System.Serializable]
    public class BossList
    {
        public List<Bosses> bossesList;
    }

    public BossList listBossList = new BossList();

    [SerializeField] List<GameObject> enemy;
    UIDisplay display;

    EnemySpawner enemySpawner;
    public int spawnedEnemies;
    LevelManager levelManager;
    int levelIndex = 0;

    private void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        levelManager = FindObjectOfType<LevelManager>();
        display = FindObjectOfType<UIDisplay>();

    }
    void Start()
    {
        levelIndex = levelManager.LevelCount();
        Spawn();
    }

    void Update()
    {
        CheckInstances();
    }

    void SpawnEnemies()
    {
        foreach (Transform child in gameObject.transform)
        {
            GameObject instance = Instantiate(listBossList.bossesList[levelIndex].bosses[Random.Range(0, listBossList.bossesList[levelIndex].bosses.Count)] , child.position, Quaternion.identity);
            spawnedEnemies++;
        }
    }

    void Spawn()
    {
        if(!enemySpawner.GetLooping())
        {
            SpawnEnemies();
        }
    }

    void CheckInstances()
    {
        Invoke("ChangeLevel", 1); 
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    void ChangeLevel()
    {
        if (spawnedEnemies <= 0)
        {
            StartCoroutine(EndLevel());
            /*enemySpawner.Level = 2;
            Invoke("Destroy", 2);*/
        }
    }

    IEnumerator EndLevel()
    {
        display.EndLevelUI();
        yield return new WaitForSeconds(5f);
        levelManager.LoadNextLevel();

    }

}
