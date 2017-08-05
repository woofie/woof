using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class BallController : MonoBehaviour {

	public float speed;

	private Rigidbody rb;
	public float dragFactor;
	public GameObject dog;

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

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}
}
