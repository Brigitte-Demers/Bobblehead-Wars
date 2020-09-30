using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Once the object goes off screen or becomes invisible.
    private void OnBecameInvisible()
    {
        // Destroys the object. In this case, the projectile.
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Destroys object upon collision.
        Destroy(gameObject);
    }
}
