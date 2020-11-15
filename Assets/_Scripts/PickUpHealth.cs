using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealth : Pickup
{
    public AudioClip pickupSFX;

    public override void PickMeUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddHealth();
        AudioManager.PlaySoundEffect(pickupSFX);
        Destroy(gameObject);
    }
}
