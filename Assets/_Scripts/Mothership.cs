using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : MonoBehaviour
{

    public int scoreValue;
    public AudioClip cruiseSFX;
    public AudioClip explodeSFX;

    private const float MAX_LEFT = -4f;
    private float speed = 5f;

    private void Start()
    {
        AudioManager.PlaySoundEffect(cruiseSFX);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);

        if (transform.position.x <= MAX_LEFT)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FriendlyBullet"))
        {
            UIManager.UpdateScore(scoreValue);
            Destroy(collision.gameObject);
            AudioManager.PlaySoundEffect(explodeSFX);
            Destroy(gameObject);
        }
    }
}
