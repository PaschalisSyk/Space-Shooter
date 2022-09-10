using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Shield : MonoBehaviour
{
    int shieldHealth = 4;
    Player player;
    [SerializeField] ParticleSystem shieldEffect;

    private void Awake()
    {
        player = FindObjectOfType<Player>();

    }

    void Start()
    {
        ShieldScale();
    }

    public void ShieldScale()
    {
        transform.DOScale(1.3f, 1f).SetEase(Ease.InFlash);
        StartCoroutine(Dissolve());
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldHealth <= 0)
        {
            Evaporate();
        }
    }

    void Evaporate()
    {
        transform.DOScale(0, 1f).SetEase(Ease.OutBack);
        //circleCollider.radius = Mathf.Lerp(0.5f, 0, 2f * Time.deltaTime);
        Invoke("Deactivate", 1.5f);
    }

    IEnumerator Dissolve()
    {
        yield return new WaitForSeconds(7f);
        Evaporate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyLaser")
        {
            Destroy(collision.gameObject);
            PlayShieldHitEffect();
            shieldHealth -= 1;
        }

        if (collision.gameObject.tag == "EnemyShip")
        {
            DamageDealer damage = collision.GetComponent<DamageDealer>();
            Destroy(collision.gameObject);
            player.GetComponent<Health>().TakeHit(damage);
            Deactivate();
        }
    }
    void Deactivate()
    {
        gameObject.SetActive(false);
        player.shieldied = false;
        shieldHealth = 4;
    }

    void PlayShieldHitEffect()
    {
        if (shieldEffect != null)
        {
            ParticleSystem instance = Instantiate(shieldEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

}

