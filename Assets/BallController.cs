using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class BallController : MonoBehaviour {

	public float speed;
	public float thrust;

	public Vector3 origin = new Vector3(0,0,0);
	private Rigidbody rb;
	public float dragFactor;
	public GameObject dog;

	private Vector3 _direction;
	private Quaternion _lookRotation;
	private float RotationSpeed;
	public float fetchDrag; 

	public string dogAction = "mobileMove";


	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.drag = dragFactor;
		//dog.GetComponent<dog> ().FollowBallToggleOn ();
	}


	void FixedUpdate()
	{
		 float moveHorizontal = CrossPlatformInputManager.GetAxis ("Horizontal");
		 float moveVertical = CrossPlatformInputManager.GetAxis ("Vertical");
//		float moveHorizontal = Input.GetAxis ("Horizontal");
//		float moveVertical = Input.GetAxis ("Vertical");


		switch (dogAction) {

		case "fetch":
			if (Vector3.Distance (transform.position, origin) > 0.5) {
				Fetch ();
			}
			break;
		case "mobileMove":
			mobileMove (-1*moveHorizontal, 0, -1*moveVertical);
			break;

		default:
			mobileMove (-1*moveHorizontal, 0, -1*moveVertical);
			break;

		}
	}

	void Fetch()
	{

		_direction = (origin - transform.position).normalized;

		//create the rotation we need to be in to look at the target
		_lookRotation = Quaternion.LookRotation(_direction);

		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);

		rb.AddForce(_direction * fetchDrag , mode: ForceMode.Impulse);
	}

	void mobileMove(float xVel, float yVel, float zVel)
	{
		Vector3 movement = new Vector3 (xVel, yVel, zVel);

		rb.AddForce (movement * speed);
	}
		
}
