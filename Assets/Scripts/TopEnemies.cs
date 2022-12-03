using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bosses")]

public class TopEnemies : ScriptableObject
{
    [SerializeField] List<GameObject> bossPref = new List<GameObject>();
    [SerializeField] List<GameObject> spawnPoints = new List<GameObject>();

    public List<GameObject> GetBosses()
    {
        return bossPref;
    }

    public List<GameObject> GetSpawnPoints()
    {
        return spawnPoints;
    }
}
