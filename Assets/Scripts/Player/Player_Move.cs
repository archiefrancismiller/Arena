using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
	public Rigidbody rb;
	Vector3 motionless = (new Vector3 (0f, 0f, 0f));
	Vector3 gravity = (new Vector3 (0f, -14f, 0f));
	public float runSpeed = 3f;
	public float airSpeed = 12f;
	public float maxSpeed = 13f;
	public float jumpForce = 30f;
	public float groundRay = 1.1f;
	public float smoothTime = 0.74f;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}
		
	void Update ()
	{

	}
		
	void FixedUpdate ()
	{
		// Prevent the Character Controller from rotating along the X and Z axes
		rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

		// Read and store WASD input and sum the axes into a Vector3
		Vector3 movement = (new Vector3 (Input.GetAxisRaw ("Horizontal"), 0.0f, Input.GetAxisRaw ("Vertical")).normalized);

		// Cast a ray downwards to check if the player is standing on solid ground
		bool grounded = Physics.Raycast(transform.position, Vector3.down, groundRay);

		// If the Character Controller is in the air, movement is limited and jumping is disabled
		if (!grounded) {
			Debug.Log ("Not grounded");
			rb.AddRelativeForce (gravity);
			rb.AddRelativeForce	((movement * airSpeed), ForceMode.Force);
		}

		// If the Character Controller is grounded, apply force in response to WASD and Jump input
		else
		{
			Debug.Log ("Grounded");
			rb.AddRelativeForce (movement * runSpeed, ForceMode.VelocityChange);

			// In the absence of WASD input, bring the Character Controller to a quick, smooth stop
			if (Input.GetAxis ("Vertical") == 0 && Input.GetAxis ("Horizontal") == 0)
			{
				rb.velocity = Vector3.Lerp (rb.velocity, motionless, smoothTime);
			}

			if (Input.GetButtonDown ("Jump"))
			{
				rb.velocity = movement += new Vector3 (0, jumpForce, 0);
			}

			// Limit maximum speed
			if(rb.velocity.magnitude > maxSpeed)
			{
				rb.velocity = rb.velocity.normalized * maxSpeed;
			}
		}
	}
}
