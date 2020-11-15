using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveObject
{ 

    public int coins;
    public int highScores;
    public ShipStats shipStats;


    public SaveObject()
    {
        coins = 0;
        highScores = 0;

        shipStats = new ShipStats();
        shipStats.maxHealth = 3;
        shipStats.maxLives = 3;
        shipStats.fireRate = 1f;
    }
}
