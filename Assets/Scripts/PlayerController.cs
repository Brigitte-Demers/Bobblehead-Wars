using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
}
