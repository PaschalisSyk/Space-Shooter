using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunUpdate : MonoBehaviour
{
    LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    private void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(UpdateGun);
    }

    private void UpdateGun()
    {
        levelManager.UpdateGun();
    }
}
