using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour 
{
	// Speed I want for rotation.
	public int rotationSpeed;
	
	void Update () 
	{
		// Sets rotation speed.
		transform.Rotate(new Vector3(0, rotationSpeed, 0)* Time.deltaTime);
	}
}
