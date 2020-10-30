//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class DeathParticles : MonoBehaviour
//{
//    // Refers to current particle system.
//    private ParticleSystem deathParticles;

//    // Lets you know the particle system has started to play.
//    private bool didStart = false;

//    // Start is called before the first frame update
//    void Start()
//    {
//        deathParticles = GetComponent<ParticleSystem>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (didStart && deathParticles.isStopped)
//        {
//            Destroy(gameObject);
//        }
//    }

//    // This starts the particle system.
//    public void Activate()
//    {
//        didStart = true;
//        deathParticles.Play();
//    }
//}
