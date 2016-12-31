// P L A Y E R _ M O V E

// Handles player locomotion
// Applies physics forces to the Character Controller in response to input
// Attach to the Character Controller

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
	public Rigidbody rb;									// The Character Controller's rigidbody component
	Vector3 Motionless = (new Vector3 (0f, 0f, 0f));		// Constant; velocity 0
	Vector3 movement;
	public float gravityConstant = -9.81f;					// Simulated gravitational force
	public float frictionCoefficient = 0.66f;				// Coefficient of friction (applied when grounded)
	public float dragCoefficient = 0.33f;					// Coefficient of air drag (applied when !grounded)

	public float groundRay = 1.1f;							// Grounding raycast range
	public float moveForce = 3f;							// Base movement force
	public float strafeModifier = 1.33f;					// Scales horizontal movement speed
	public float jumpForce = 30f;							// Jump force


	private float horizontalInput;							// Stores raw Horizontal (AD) input
	private float verticalInput;							// Stores raw Vertical (SW) input
	private float airHorizontalInput;						// Stores a version of Horizontal (AD) input, modified by drag
	private float airVerticalInput;							// Stores a version of Vertical (AD) input, modified by drag

	// Called on initialization
	void Start ()
	{
		// Get the Character Controller's Rigidbody component
		rb = GetComponent<Rigidbody>();

		// Freeze rotation in the X and Z axes (so it doesn't tip over)
		rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
	}

	// Called every frame
	void Update ()
	{
		// Read and store WASD input; apply strafe modifier to Horizontal axis
		horizontalInput = Input.GetAxisRaw ("Horizontal") * strafeModifier;
		verticalInput = Input.GetAxisRaw ("Vertical");

		// Cast a ray towards the ground to see if the Walker is grounded
		bool grounded = Physics.Raycast(transform.position, Vector3.down, groundRay);

		if (Input.GetButtonDown ("Jump") && grounded)
		{
			rb.velocity += jumpForce * Vector3.up;
		}
	}

	// Called every physics frame (stepped)
	void FixedUpdate ()
	{
		// Store an alternate version of processed WASD input with drag applied
		airHorizontalInput = horizontalInput * dragCoefficient;
		airVerticalInput = verticalInput * dragCoefficient;

		// Cast a ray towards the ground to see if the Walker is grounded
		bool grounded = Physics.Raycast(transform.position, Vector3.down, groundRay);

		// If the Character Controller is (grounded), apply force in response to WASD and Jump input
		if (grounded)
		{
			// Print grounding status to log
			Debug.Log ("Grounded");

			// Create the Vector3 for (grounded) movement
			Vector3 movement = (new Vector3 (horizontalInput, 0.0f, verticalInput));

			// Apply movement force
			rb.AddRelativeForce (movement * moveForce, ForceMode.Acceleration);

			// Apply simulated friction
			rb.velocity = Vector3.Lerp (rb.velocity, Motionless, frictionCoefficient);
		}

		// If the Character Controller is (!grounded)...
		else
		{

			// Print grounding status to log
			Debug.Log ("Not grounded");

			// Create the Vector3 for (!grounded) movement
			Vector3 airMovement = (new Vector3 (airHorizontalInput, gravityConstant, airVerticalInput));

			// Apply limited movement force
			rb.AddRelativeForce ((airMovement * moveForce), ForceMode.Acceleration);
		}
	}
}
