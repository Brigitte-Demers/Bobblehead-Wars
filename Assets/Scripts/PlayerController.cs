using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // The marine's body.
    public Rigidbody marineBody;

    // Provides an array of force values for the camera.
    public float[] hitForce;

    // Grace period after the hero sustains damage. 
    public float timeBetweenHits = 2.5f;

    // Base movement speed
    public float moveSpeed = 50.0f;

    public Rigidbody head;

    // Indicates what layer the mask should hit.
    public LayerMask layerMask;

    public Animator bodyAnimator;

    // Instance variable to store the CharacterController.
    private CharacterController characterController;

    // Where the Marine should be staring.
    private Vector3 currentLookTarget = Vector3.zero;

    // A flag that indicates the hero took a hit.
    private bool isHit = false;

    // Tracks amount of time in the grace period.
    private float timeSinceHit = 0;

    // Number of times the hero took a hit.
    private int hitNumber = -1;

    // Keeps track of the players current death state.
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        // Gets reference to current component passed into the script.
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Prevents the character from goin through obstacles. 
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"),
            0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection * moveSpeed);

        if (isHit)
        {
            timeSinceHit += Time.deltaTime;
            if (timeSinceHit > timeBetweenHits)
            {
                isHit = false;
                timeSinceHit = 0;
            }
        }
    }

    void FixedUpdate()
    {
        // Makes the Marine's head boble back and forth.
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"),
            0, Input.GetAxis("Vertical"));
        if (moveDirection == Vector3.zero)
        {
            bodyAnimator.SetBool("IsMoving", false);
        }
        else
        {
            // Adds the force behind the motion of the Marine's head.
            head.AddForce(transform.right * 150, ForceMode.Acceleration);

            bodyAnimator.SetBool("IsMoving", true);
        }

        // Creates empty RaycastHit.
        RaycastHit hit;

        // Populates the RaycastHit with an object if hit.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Draws the ray so it is visible within the scene.
        UnityEngine.Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);

        // Physics.Raycast casts the ray. 1000 indicates the length of the ray.
        // layerMask lets the cast know what you're attempting to hit.
        if (Physics.Raycast(ray, out hit, 1000, layerMask,

            // QueryTriggerInteraction.Ignore tells the physics engine to not activate
            // any of the triggers.
            QueryTriggerInteraction.Ignore))
        {
            // Activates if it is NOT equal to the currentLookTarget.
            if (hit.point !=currentLookTarget)
            {
                // Coordinates the raycast hits/where the Marine should be looking.
                currentLookTarget = hit.point;

                // 1: Gets target position.
                Vector3 targetPosition = new Vector3(hit.point.x,
                    transform.position.y, hit.point.z);

                // 2: Calculates the Quaternions which will determine the rotation.
                Quaternion rotation = Quaternion.LookRotation(targetPosition -
                    transform.position);

                // 3: Does the actual turning by using Lerp.
                transform.rotation = Quaternion.Lerp(transform.rotation,
                    rotation, Time.deltaTime * 10.0f);
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        Alien alien = other.gameObject.GetComponent<Alien>();
        if (alien != null)
        {
            // Check if colliding object has an Alien script attatched. If it is an Alien and it hits 
            // the player, the player is considered hit.
            if (!isHit)
            {
                // Increases hit number by 1, then gets a reference to CameraShake().
                hitNumber += 1;
                CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
                // If the current hitNumber is les then the number of force values for the camera shake, then the 
                // hero is still alive.
                if (hitNumber < hitForce.Length)
                {
                    cameraShake.intensity = hitForce[hitNumber];
                    cameraShake.Shake();
                }
                else
                {
                    Die();
                }
                // This sets hit to true, plays the grunt sound and kills the Alien.
                isHit = true;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.hurt);
            }
            alien.Die();
        }
    }

    public void Die()
    {
        bodyAnimator.SetBool("IsMoving", false);
        marineBody.transform.parent = null;
        marineBody.isKinematic = false;
        marineBody.useGravity = true;
        marineBody.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        marineBody.gameObject.GetComponent<Gun>().enabled = false;

        Destroy(head.gameObject.GetComponent<HingeJoint>());
        head.transform.parent = null;
        head.useGravity = true;
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.marineDeath);
        Destroy(gameObject);
    }
}
