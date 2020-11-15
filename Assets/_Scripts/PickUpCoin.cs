using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCoin : Pickup
{
    public AudioClip pickupSFX;

    public override void PickMeUp()
    {
        Inventory.currentCoins++;
        AudioManager.PlaySoundEffect(pickupSFX);
        UIManager.UpdateCoins();
        Destroy(gameObject);
    }

}
