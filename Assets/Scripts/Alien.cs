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

    // Start is called before the first frame update
    void Start()
    {
        // This gets a reference to the NavMeshAgent so I can access it in code.
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navigationTime += Time.deltaTime;
        if (navigationTime > navigationUpdate)
        {
            agent.destination = target.position;
            navigationTime = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Die();

        // Cals the sound effect for the aliens death upon the Alien game objects
        // destruction.
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
    }

    public void Die()
    {
        OnDestroy.Invoke();
        OnDestroy.RemoveAllListeners();
        Destroy(gameObject);
    }
}
