using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10;
    [SerializeField] float baseProjectileFiringPeriod = 0.2f;
    [SerializeField] float projectileLifeTime = 5f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRangeVariance = 0;
    [SerializeField] float minimunFiringRange = 0.3f;

    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(useAI)
        {
            isFiring = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            Destroy(laser, projectileLifeTime);

            Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();

            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            float timeToNextProjectile = Random.Range(baseProjectileFiringPeriod - firingRangeVariance,
                                                      baseProjectileFiringPeriod + firingRangeVariance);

            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimunFiringRange, float.MaxValue);

            audioPlayer.PLayShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
