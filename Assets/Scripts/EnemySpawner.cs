using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Waves
    {
        public List<WaveConfig> _waves;
    }
    [System.Serializable]
    public class WavesList
    {
        public List<Waves> wavesList;
    }

    public WavesList listWavesList = new WavesList();


    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] List<WaveConfig> wave2;
    [SerializeField] int startingWave = 0;
    [SerializeField] public bool looping = false;
    [SerializeField] GameObject topEnemiesSpawner;
    float timeBetweenWaves = 2f;
    LevelManager levelManager;
    [SerializeField] int count;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        count = levelManager.levelCount;

    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        
        do
        {
            yield return StartCoroutine(SpawnAllWaves(listWavesList.wavesList[count]._waves));

        } while (looping);
    }

    private IEnumerator SpawnAllWaves(List<WaveConfig> waves)
    {
        for (int waveIndex= startingWave; waveIndex < waves.Count; waveIndex++)
        {
            var currentWave = waves[waveIndex];
            yield return new WaitForSeconds(timeBetweenWaves);
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));

        }
        looping = false;
        yield return new WaitForSeconds(timeBetweenWaves);
        Instantiate(topEnemiesSpawner, transform.position, Quaternion.identity);
        //count++;
        
    }
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWayPoints()[0].transform.position,
                Quaternion.Euler(0,0,180));
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    public int GetWavesCount()
    {
        return waveConfigs.Count;
    }

    public bool GetLooping()
    {
        return looping;
    }

    void SpawnWaves(int index)
    {
        StartCoroutine(SpawnAllWaves(listWavesList.wavesList[index]._waves));
    }

}
