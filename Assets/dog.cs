using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class dog : MonoBehaviour {

	private Rigidbody rb;
	private Animation anim;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animation> ();
	}

	// Update is called once per frame
	void Update () {
		float x = CrossPlatformInputManager.GetAxis ("Horizontal");
		float y = CrossPlatformInputManager.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (x, 0, y);

		rb.velocity = movement * 4f;

		if (x != 0 && y != 0) {
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.Atan2 (x, y) * Mathf.Rad2Deg, transform.eulerAngles.z);

		}

		if (x != 0 || y != 0) {
			Debug.Log ("should play walk");
			anim.Play ("CorgiWalk");

		} else {
			Debug.Log ("Should stay");
			anim.Play ("CorgiDeath");
		}

	}
}
