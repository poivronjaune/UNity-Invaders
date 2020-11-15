using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public ShipStats shipStats;
    public AudioClip shootSFX;

    private const float MAX_RIGHT = 2.5f;
    private const float MAX_LEFT = -2.5f;

    private Vector2 offScreenPosition = new Vector2(0, -20f);
    private Vector2 startingPosition = new Vector2(0, -4.2f);

    private bool isShooting = false;

    private bool moveLeft = false;
    private bool moveRight = false;


    private void Start()
    {
        shipStats.currentHealth = shipStats.maxHealth;
        shipStats.currentLives = shipStats.maxLives;

        transform.position = startingPosition;

        UIManager.UpdateHealthbar(shipStats.currentHealth);
        UIManager.UpdateLives(shipStats.currentLives);

    }


    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A) && transform.position.x > MAX_LEFT)
            transform.Translate(Vector2.left * Time.deltaTime * shipStats.shipSpeed);

        if (Input.GetKey(KeyCode.D) && transform.position.x < MAX_RIGHT)
            transform.Translate(Vector2.right * Time.deltaTime * shipStats.shipSpeed);

        if (Input.GetKey(KeyCode.Space) && !isShooting)
            StartCoroutine(Shoot());
#endif

        if (moveLeft && transform.position.x > MAX_LEFT)
            transform.Translate(Vector2.left * Time.deltaTime * shipStats.shipSpeed);
        if (moveRight && transform.position.x < MAX_RIGHT)
            transform.Translate(Vector2.right * Time.deltaTime * shipStats.shipSpeed);


    }

    private void TakeDamage()
    {
        shipStats.currentHealth--;
        UIManager.UpdateHealthbar(shipStats.currentHealth);

        if (shipStats.currentHealth <= 0)
        {
            looseLife();
        }
    }

    public void looseLife()
    {
        shipStats.currentLives--;
        UIManager.UpdateLives(shipStats.currentLives);
        if (shipStats.currentLives <= 0)
        {
            // Game Over
            SaveManager.SaveProgress();
            Debug.Log("Game Over!");
        }
        else
        {
            // Respawn Player
            StartCoroutine(Respawn());
            Debug.Log("Respawn player");
        }
    }

    public void AddHealth()
    {
        if (shipStats.currentHealth == shipStats.maxHealth)
        {
            UIManager.UpdateScore(250);
        }
        else
        {
            shipStats.currentHealth++;
            UIManager.UpdateHealthbar(shipStats.currentHealth);
        }
    }

    public void AddLife()
    {
        if (shipStats.currentLives == shipStats.maxLives)
        {
            UIManager.UpdateScore(1000);
        }
        else
        {
            shipStats.currentLives++;
            UIManager.UpdateLives(shipStats.currentHealth);
        }
    }

    private IEnumerator Shoot()
    {

        isShooting = true;
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        AudioManager.PlaySoundEffect(shootSFX);
        yield return new WaitForSeconds(shipStats.fireRate);
        isShooting = false;
    }


    private IEnumerator Respawn()
    {
        transform.position = offScreenPosition;
        yield return new WaitForSeconds(2);

        shipStats.currentHealth = shipStats.maxHealth;
        UIManager.UpdateHealthbar(shipStats.currentHealth);
        transform.position = startingPosition;

    }


    public void LeftButtonDown()
    {
        moveLeft = true;
    }
    public void RightButtonDown()
    {
        moveRight = true;
    }
    public void DirectionReleased()
    {
        moveLeft = false;
        moveRight = false;
    }
    public void ShootButton()
    {
        if (!isShooting)
            StartCoroutine(Shoot());
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet")) 
        {
            Destroy(collision.gameObject);
            TakeDamage();
            Debug.Log("Player hit, health:" + shipStats.currentHealth + " | lives:" + shipStats.currentLives);
        }

    }

}
