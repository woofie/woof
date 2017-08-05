using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class dog : MonoBehaviour {

	private Rigidbody rb;
	private Animation anim;
	private GameObject ball;
	private bool followBall;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animation> ();
		followBall = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (followBall) {
			transform.position = Vector3.MoveTowards (transform.position, ball.transform.position, .03f);
		}
	}

	public void FollowBallToggleOn (GameObject ball){
		followBall = true;
		this.ball = ball;
	}
}
