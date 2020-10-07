using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Creates public variable, visible within the Unity editor.
    public GameObject bulletPrefab;

    // Creates public variable for the launch point.
    public Transform launchPosition;

    // a flag that lets the script know whether to fire one or three bullets.
    public bool isUpgraded;

    // How long the upgrade will last. (In seconds).
    public float upgradeTime = 10.0f;

    // Creates an audio instance.
    private AudioSource audioSource;

    // Keeps track of how long it's been since the gun was upgraded.
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        // Gets a reference to the attached AudioSource.
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // If mouse button is being pressed down.
        if (Input.GetMouseButtonDown(0))
        {
            // Fires projectiles if the mouse button is being pressed down.
            if (!IsInvoking("fireBullet"))
            {
                // Fires the projectiles(bullet prefabs).
                InvokeRepeating("fireBullet", 0f, 0.1f);
            }
        }
        // If mouse button is not being pressed.
        if (Input.GetMouseButtonUp(0))
        {
            // This makes the gun stop firing once the mouse button is released.
            CancelInvoke("fireBullet");
        }

        // Increments the time so the player does not have the upgrade for too long.
        currentTime += Time.deltaTime;
        if (currentTime > upgradeTime && isUpgraded == true)
        {
            isUpgraded = false;
        }
    }

    void fireBullet()
    {
        Rigidbody bullet = createBullet();

        // 3: Specifies the velocity for the bullet prefab.
        bullet.velocity = transform.parent.forward * 100;

        // Plays the shooting sound.
        audioSource.PlayOneShot(SoundManager.Instance.gunFire);

        // Fires the next two bullets at angles.
        if (isUpgraded)
        {
            Rigidbody bullet2 = createBullet();
            bullet2.velocity =
                (transform.right + transform.forward / 0.5f) * 100;
            Rigidbody bullet3 = createBullet();
            bullet3.velocity =
                ((transform.right * -1) + transform.forward / 0.5f) * 100;
        }

        // Integrates the sound.
        if (isUpgraded)
        {
            audioSource.PlayOneShot(SoundManager.Instance.upgradedGunFire);
        }
        else
        {
            audioSource.PlayOneShot(SoundManager.Instance.gunFire);
        }
    }

    // This method just encapsulates the bullet creation process.
    private Rigidbody createBullet()
    {
        // 1: Creates game object instance for the bullet prefab.
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;

        // 2: Sets bullets position to the launcher's position.
        bullet.transform.position = launchPosition.position;
        return bullet.GetComponent<Rigidbody>();
    }

    // This method lets the gun know it has been upgraded.
    public void UpgradeGun()
    {
        isUpgraded = true;
        currentTime = 0;
    }
}