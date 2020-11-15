using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public int scoreValue;

    public GameObject lifePrefab;
    public GameObject healthPrefab;
    public GameObject coinPrefab;
    public AudioClip explodeSFX;

    public GameObject explosion;

    private const int LIFE_CHANCE = 1;
    private const int HEALTH_CHANCE = 10;
    private const int COIN_CHANCE = 100;

    public void Kill()
    {
        UIManager.UpdateScore(scoreValue);
        Instantiate(explosion, transform.position, Quaternion.identity);
        AudioManager.PlaySoundEffect(explodeSFX);
        Destroy(gameObject);
        AlienMaster.allAliens.Remove(gameObject);

        int rand = Random.Range(0, 1000);
        if (rand == LIFE_CHANCE)
            Instantiate(lifePrefab, transform.position, Quaternion.identity);
        else if (rand <= HEALTH_CHANCE)
            Instantiate(healthPrefab, transform.position, Quaternion.identity);
        else if (rand <= COIN_CHANCE)
            Instantiate(coinPrefab, transform.position, Quaternion.identity);

        AudioManager.UpdateBattleMusicDelay(AlienMaster.allAliens.Count);

        if (AlienMaster.allAliens.Count == 0)
            GameManager.SpawnNewWave();

    }



}
