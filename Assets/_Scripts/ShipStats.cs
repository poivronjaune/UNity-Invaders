using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipStats
{
    [Range(1,5)]
    [HideInInspector]
    public int maxHealth = 3;
    [HideInInspector]
    public int currentHealth;
    [HideInInspector]
    public int maxLives = 3;
    [HideInInspector]
    public int currentLives = 3;
    [HideInInspector]
    public float shipSpeed;
    [HideInInspector]
    public float fireRate;


}
