using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopEnemySpawner : MonoBehaviour
{
    [SerializeField] List<TopEnemies> topEnemies;

    UIDisplay display;

    EnemySpawner enemySpawner;
    public int spawnedEnemies;
    LevelManager levelManager;
    int levelIndex = 0;
    Player player;

    private void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        levelManager = FindObjectOfType<LevelManager>();
        display = FindObjectOfType<UIDisplay>();
        player = FindObjectOfType<Player>();

    }
    void Start()
    {
        levelIndex = levelManager.LevelCount() -1;
        if(levelIndex < 0)
        {
            levelIndex = 0;
        }
        Spawn(topEnemies[levelIndex]);
    }

    void Update()
    {
        CheckInstances();
    }

    void SpawnEnemies(TopEnemies topEnemies)
    {
        int index = 0;

        for (int i = 0; i < topEnemies.GetSpawnPoints().Count; i++)
        {
            var spawnPoint = Instantiate(topEnemies.GetSpawnPoints()[i], topEnemies.GetSpawnPoints()[i].transform.position, Quaternion.identity);
            spawnPoint.transform.SetParent(this.transform);
        }

        foreach (Transform child in gameObject.transform)
        {
            
            //GameObject instance = Instantiate(listBossList.bossesList[levelIndex].bosses[Random.Range(0, listBossList.bossesList[levelIndex].bosses.Count)] , child.position, Quaternion.identity);
            GameObject instance = Instantiate(topEnemies.GetBosses()[index], child.position, Quaternion.identity);
            spawnedEnemies++;
            index++;
        }
    }

    void Spawn(TopEnemies topEnemies)
    {
        if(!enemySpawner.GetLooping())
        {
            SpawnEnemies(topEnemies);
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
        }
    }

    IEnumerator EndLevel()
    {
        display.EndLevelUI();
        player.IsFiring();
        yield return new WaitForSeconds(2f);
    }

}
