using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Main enemies script to move aliens left and right and down.
/// Also the speed of the movement should increase as aliens drop dead
/// 
/// </summary>


public class AlienMaster : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject mothershipPrefab;
    public float MAX_LEFT = -2.5f;
    public float MAX_RIGHT = 2.5f;

    private Vector3 hMoveDistance = new Vector3(0.03f,0,0);
    private Vector3 vMoveDistance = new Vector3(0,0.08f, 0);
    private Vector3 motherShipSpawnPos = new Vector3(3.16f,4.37f,0);


    private const float MAX_MOVE_SPEED = 0.02f;

    private float moveTimer = 0.05f;
    private const float moveTime = 0.05f;  // Multiply by number of enemies on screen

    private float shootTimer = 3f;
    private const float shootTime = 3f;

    private float mothershipTimer = 1f;
    private const float MOTHERSHIP_MIN = 10f;
    private const float MOTHERSHIP_MAX = 30f;


    public static List<GameObject> allAliens = new List<GameObject>();
    private bool movingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Alien") )
        {
            allAliens.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Instantiate(bulletPrefab, new Vector3(0, 0.87f,0), Quaternion.identity);


        if (moveTimer <= 0)
            MoveEnemies();

        if (shootTimer <= 0)
            Shoot();

        if (mothershipTimer <= 0)
            SpawnMothership();

        moveTimer -= Time.deltaTime;
        shootTimer -= Time.deltaTime;
        mothershipTimer -= Time.deltaTime;

    }

    private void MoveEnemies()
    {
        if (allAliens.Count > 0)
        {
            int hitMax = 0;
            for (int i = 0; i < allAliens.Count; i++)
            {
                if (movingRight)
                  allAliens[i].transform.position += hMoveDistance;
                else
                  allAliens[i].transform.position -= hMoveDistance;

                if (allAliens[i].transform.position.x > MAX_RIGHT || allAliens[i].transform.position.x < MAX_LEFT)
                    hitMax++;
            }
            if (hitMax > 0)
            {
                for (int i = 0; i < allAliens.Count; i++)
                    allAliens[i].transform.position -= vMoveDistance;
                movingRight = !movingRight;
            }
            moveTimer = GetMoveSpeed();
        }
    }

    private void Shoot()
    {
        // Find a valide poit to shoot from aliens
        Vector2 pos = allAliens[Random.Range(0, allAliens.Count)].transform.position;
        Instantiate(bulletPrefab, pos, Quaternion.identity);

        // Add bullet move scriptinpg here

        shootTimer = shootTime;
    }

    private void SpawnMothership()
    {
        Instantiate(mothershipPrefab, motherShipSpawnPos, Quaternion.identity);
        mothershipTimer = Random.Range(MOTHERSHIP_MIN, MOTHERSHIP_MAX);


    }

    private float GetMoveSpeed()
    {
        float f = allAliens.Count * moveTimer;

        if (f < MAX_MOVE_SPEED)
            return MAX_MOVE_SPEED;
        else
            return f;
    }



}
