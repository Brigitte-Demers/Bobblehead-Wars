using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaWall : MonoBehaviour
{
    private Animator arenaAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // This gets the parent GameObject by accessing the parent property on 
        // the transform.
        GameObject arena = transform.parent.gameObject;

        // This calls the GetComponent() for a reference to the animator.
        arenaAnimator = arena.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When the trigger is activated, the code "IsLowred" to true.
    void OnTriggerEnter(Collider other)
    {
        arenaAnimator.SetBool("IsLowered", true);
    }

    // When the hero leaves the trigger, this tells the Animator to set "IsLowered" to
    // false, which raises the walls.
    void OnTriggerExit(Collider other)
    {
        arenaAnimator.SetBool("IsLowered", false);
    }
}
