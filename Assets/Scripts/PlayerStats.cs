using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] Sprite sprite;
    [SerializeField] Color color;
    [SerializeField] float movespeed;

    public Sprite GetSprite()
    {
        return sprite;
    }

    public float GetMoveSpeed()
    {
        return movespeed;
    }
    
    public Color GetColor()
    {
        return color;
    }
}
