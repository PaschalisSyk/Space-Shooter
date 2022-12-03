using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 10;
    Health health;

    private void Awake()
    {
        health = FindObjectOfType<Health>();
    }

    public int GetDamage()
    {
        return damage;
    }
    
    public void Hit()
    {
        if(!health.isPlayer)
        {
            Destroy(gameObject);
        }

    }

}
