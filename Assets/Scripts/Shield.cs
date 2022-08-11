using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Shield : MonoBehaviour
{
    int shieldHealth = 4;
    CircleCollider2D circleCollider;
    Player player;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        player = FindObjectOfType<Player>();

    }

    void Start()
    {
        //circleCollider.radius = 0.5f;
        ShieldScale();
    }

    public void ShieldScale()
    {
        transform.DOScale(1.3f, 1f).SetEase(Ease.InCubic);
        StartCoroutine(Dissolve());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Evaporate()
    {
        transform.DOScale(0, 2).SetEase(Ease.InElastic, 1);
        circleCollider.radius = Mathf.Lerp(0.5f, 0, 2f * Time.deltaTime);
        Invoke("Deactivate", 3f);
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
            shieldHealth -= 1;
        }

        if (collision.gameObject.tag == "EnemyShip")
        {
            Destroy(collision.gameObject);
            Deactivate();
        }

        if (shieldHealth <= 0)
        {
            Evaporate();
        }
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
        player.shieldied = false;
    }

}

