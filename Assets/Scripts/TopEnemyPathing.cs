using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopEnemyPathing : MonoBehaviour
{
    GameObject player;
    [SerializeField] float movespeed;
    TopEnemySpawner spawner;
    Vector2 target;
    Vector2 offset = new Vector2(0, 2);
    bool _active;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawner = FindObjectOfType<TopEnemySpawner>();
    }
    void Start()
    {

    }

    void Update()
    {
        MoveTo();

    }

    private void OnDestroy()
    {
        spawner.spawnedEnemies--;
    }

    void MoveTo()
    {
        if (player != null)
        {
            transform.up = player.transform.position - transform.position;
            target = player.transform.position;
            this.transform.position = Vector2.Lerp(transform.position, target + offset, 0.1f * movespeed * Time.deltaTime);
        }
    }

}