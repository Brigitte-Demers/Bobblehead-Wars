using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 Allows access to UnityEvent in code.
 */
using UnityEngine.Events;

using UnityEngine.AI;

public class Alien : MonoBehaviour
{
    // Will help launch the head.
    public Rigidbody head;

    // Tracks the Alien's state.
    public bool isAlive = true;

    // Custom event type that can be configured in the inspector. Will occur on each
    // call to an alien.
    public UnityEvent OnDestroy;

    // Target is where the alien should go.
    public Transform target;

    // Amount of time, in milliseconds, for when the alien should update its path.
    public float navigationUpdate;

    private NavMeshAgent agent;

    // Tracks how much time has passed since the previous update?
    private float navigationTime = 0;

    //private DeathParticles deathParticles;

    // Start is called before the first frame update
    void Start()
    {
        // This gets a reference to the NavMeshAgent so I can access it in code.
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            navigationTime += Time.deltaTime;
            if (navigationTime > navigationUpdate)
            {
                agent.destination = target.position;
                navigationTime = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isAlive)
        {
            Die();
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
        }
    }

    public void Die()
    {
        isAlive = false;
        head.GetComponent<Animator>().enabled = false;
        head.isKinematic = false;
        head.useGravity = true;
        head.GetComponent<SphereCollider>().enabled = true;
        head.gameObject.transform.parent = null;
        head.velocity = new Vector3(0, 26.0f, 3.0f);
        head.GetComponent<SelfDestruct>().Initiate();

        //if (deathParticles)
        //{
        //    deathParticles.transform.parent = null;
        //    deathParticles.Activate();
        //}
    }

    //public DeathParticles GetDeathParticles()
    //{
    //    if (deathParticles == null)
    //    {
    //        deathParticles = GetComponentInChildren<DeathParticles>();
    //    }
    //    return deathParticles;
    //}
}
