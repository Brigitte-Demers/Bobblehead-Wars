using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public Gun gun;

    // When player collides with the power-up, this sets the gun to Upgrade mode, and 
    // it will destroy itself. Also plays a sound when the player picks up the upgrade.
    void OnTriggerEnter(Collider other)
    {
        gun.UpgradeGun();
        Destroy(gameObject);
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpPickup);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
