using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // What I want the camera to follow.
    public GameObject followTarget;
    // The speed I want the camera to be following at.
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checks to see if there is an availible target for the camera to follow.
        // If not, the camera does not follow.
        if (followTarget != null)
        {
            // .Lerp() is called to calculate the required position of the CameraMount.
            transform.position = Vector3.Lerp(transform.position,
                followTarget.transform.position, Time.deltaTime * moveSpeed);
        }
    }
}
