using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Creates public variable, visible within the Unity editor.
    public GameObject bulletPrefab;
    // Creates public variable for the launch point.
    public Transform launchPosition;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    void fireBullet ()
    {
        // 1: Creates game object instance for the bullet prefab.
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        // 2: Sets bullets position to the launcher's position.
        bullet.transform.position = launchPosition.position;
        // 3: Specifies the direction and velocity for the bullet prefab.
        bullet.GetComponent<Rigidbody>().velocity =
            transform.parent.forward * 100;
    }
}
