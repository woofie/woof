using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

	public float speed;

	private Rigidbody rb;
	public GameObject dog;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		dog.GetComponent<dog> ().FollowBallToggleOn (this.dog);
	}


	void FixedUpdate()
	{
		float moveHorizontal = CrossPlatformInputManager.GetAxis ("Horizontal");
		float moveVertical = CrossPlatformInputManager.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}
}
