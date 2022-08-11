using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] GameObject _shield;

    [SerializeField] bool applyCameraShake;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    Shield shield;
    Player player;


    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
        shield = FindObjectOfType<Shield>();
        player = FindObjectOfType<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null && !player.shieldied)
        {
            TakeDamage(damageDealer.GetDamage());
            audioPlayer.PlayDamageSound();
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    private void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Destruction();
        }
    }

    public void Destruction()
    {
        if(!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
        }
        else
        {
            health = 0;
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);

    }

    void PlayHitEffect()
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

}
