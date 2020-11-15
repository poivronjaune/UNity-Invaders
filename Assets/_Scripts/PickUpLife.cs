using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpLife : Pickup
{
    public AudioClip pickupSFX;

    public override void PickMeUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddLife();
        AudioManager.PlaySoundEffect(pickupSFX);
        Destroy(gameObject);
    }
}
