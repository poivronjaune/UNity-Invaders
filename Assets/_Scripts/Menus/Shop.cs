using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class Shop : MonoBehaviour
{

    public AudioClip noSale;
    public AudioClip sale;

    public TextMeshProUGUI currentCoins;
    public TextMeshProUGUI healthValues;
    public TextMeshProUGUI fireRateValues;
    public TextMeshProUGUI healthCost;
    public TextMeshProUGUI fireRateCost;

    public Button healthButton;
    public Button fireRateButton;

    private int currentMaxHealth;
    private float currentFireRate;

    private int nextHealthCost;
    private int nextFireRateCost;

    private int maxHealthMultiplier = 5;
    private int fireRateMultiplier = 5;

    private int maxHealthBaseCost = 10;
    private int fireRateBaseCost = 5;

    private Player player;

    // Start is called before the first frame update
    private void Start()
    {
        currentCoins.text = Inventory.currentCoins.ToString() + " Coins";
    }

    void StartOld()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        currentMaxHealth = player.shipStats.currentHealth;
        currentFireRate = player.shipStats.fireRate;
        currentCoins.text = Inventory.currentCoins + " coins";

        UpdateUIAndTotals();

    }

    public void BuyHealth()
    {
        if (PriceCheck(nextHealthCost))
        {
            Inventory.currentCoins -= nextHealthCost;
            currentCoins.text = Inventory.currentCoins.ToString() + " Coins";

            player.shipStats.maxHealth++;
            currentMaxHealth = player.shipStats.maxHealth;
            SaveManager.SaveProgress();
            UpdateUIAndTotals();
            AudioManager.PlaySoundEffect(sale);
        }
        else
        {
            AudioManager.PlaySoundEffect(noSale);
        }
    }

    public void BuyFireRate()
    {
        if (PriceCheck(nextFireRateCost))
        {
            Inventory.currentCoins -= nextFireRateCost;
            currentCoins.text = Inventory.currentCoins.ToString() + " Coins";

            player.shipStats.fireRate -= 0.1f;
            currentFireRate = player.shipStats.fireRate;
            SaveManager.SaveProgress();
            UpdateUIAndTotals();
            AudioManager.PlaySoundEffect(sale);
        }
        else
        {
            AudioManager.PlaySoundEffect(noSale);
        }
    }

    private void UpdateUIAndTotals()
    {
        currentCoins.text = Inventory.currentCoins.ToString() + " Coins";
        if (currentMaxHealth < 5) 
        {
            nextHealthCost = currentMaxHealth * (maxHealthBaseCost * maxHealthMultiplier);
            healthValues.text = currentMaxHealth + " => " + (currentMaxHealth + 1);
            healthCost.text = nextHealthCost + "Coins";
            healthButton.interactable = true;
        }
        else
        {
            healthCost.text = "MAX";
            healthValues.text = currentMaxHealth.ToString();
            healthButton.interactable = false;
        }

        if (currentFireRate > 0.2f)
        {
            nextFireRateCost = 0;
            for (float f = 1; f > 0.02f; f -= 0.1f)
            {
                nextFireRateCost = (fireRateBaseCost * fireRateMultiplier);
                if (f <= currentFireRate)
                    break;
            }
            fireRateValues.text = currentFireRate.ToString("0.00") + " => " + (currentFireRate - 0.1f).ToString("0.00");
            fireRateCost.text = nextFireRateCost + "Coins";
            fireRateButton.interactable = true;
        }
        else
        {
            fireRateCost.text = "MAX";
            fireRateValues.text = "0.20";
            fireRateButton.interactable = false;
        }

    }

    private bool PriceCheck(int cost)
    {
        if (Inventory.currentCoins >= cost)
            return true;
        else
            return false;
    }


#if UNITY_EDITOR
    [MenuItem("Cheats/Add Coins")]
    private static void AddCoinsCheat()
    {
        Inventory.currentCoins = 1000;

    }
#endif

}
