using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        LoadProgress();
    }

    public static void SaveProgress()
    {
        SaveObject so = new SaveObject();

        so.coins = Inventory.currentCoins;
        so.highScores = UIManager.GetHighscore();
        so.shipStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().shipStats;

        SaveLoad.SaveState(so);
    }

    public static void LoadProgress()
    {
        SaveObject so = SaveLoad.LoadState();

        Inventory.currentCoins = so.coins;
        UIManager.UpdateHighscore(so.highScores);

        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().shipStats = so.shipStats;
        //Debug.Log("Local storage folder " + Application.persistentDataPath);
        //Debug.Log("Coins loaded: " + so.coins.ToString());
        //Debug.Log("High score loaded: " + so.highScores.ToString());
        //Debug.Log("Ship stats - maxHealth: " + so.shipStats.maxHealth.ToString());
        //Debug.Log("Ship stats - maxLives: " + so.shipStats.maxLives.ToString());
        //Debug.Log("Ship stats - shipSpeed: " + so.shipStats.shipSpeed.ToString());
        //Debug.Log("Ship stats - fireRate: " + so.shipStats.fireRate.ToString());
    }

}
