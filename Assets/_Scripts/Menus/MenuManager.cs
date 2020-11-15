using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject gameOverMenu;
    public GameObject shopMenu;
    public GameObject pauseMenu;
    public GameObject inGameMenu;

    public static MenuManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ReturnToMainMenu();
    }

    public void OpenMainMenu()
    {
        instance.mainMenu.SetActive(true);
        instance.inGameMenu.SetActive(false);
    }

    public static void OpenGameOver()
    {
        instance.gameOverMenu.SetActive(true);
        instance.inGameMenu.SetActive(false);
    }


    public void OpenPause()
    {
        instance.pauseMenu.SetActive(true);
        instance.inGameMenu.SetActive(false);
        Time.timeScale = 0;
    }

    public void ClosePause()
    {
        instance.pauseMenu.SetActive(false);
        instance.inGameMenu.SetActive(true);
        Time.timeScale = 1;
    }


    public void OpenShop()
    {
        instance.mainMenu.SetActive(false);
        instance.shopMenu.SetActive(true);

    }
    public void CloseShop()
    {
        instance.mainMenu.SetActive(true);
        instance.shopMenu.SetActive(false);
    }


    public void OpenInGame()
    {
        Time.timeScale = 1;
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        player.shipStats.currentHealth = player.shipStats.maxHealth;
        Debug.Log(player.shipStats.currentHealth);
        UIManager.UpdateHealthbar(player.shipStats.currentHealth);
        //player.shipStats.currentLives = player.shipStats.maxLives;
        //UIManager.UpdateLives(player.shipStats.currentLives);

        //instance.gameOverMenu.SetActive(false);
        //instance.pauseMenu.SetActive(false);
        //instance.shopMenu.SetActive(false);
        instance.mainMenu.SetActive(false);
        
        instance.inGameMenu.SetActive(true);
        GameManager.SpawnNewWave();

    }

    public void ReturnToMainMenu()
    {
        instance.gameOverMenu.SetActive(false);
        instance.pauseMenu.SetActive(false);
        instance.shopMenu.SetActive(false);
        instance.inGameMenu.SetActive(false);

        instance.mainMenu.SetActive(true);
        GameManager.CancelGame();
        Time.timeScale = 1;
    }

    public static void CloseWindow(GameObject go)
    {
        go.SetActive(false);
    }


}
