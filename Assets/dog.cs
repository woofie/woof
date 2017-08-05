using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class dog : MonoBehaviour {

	private Rigidbody rb;
	private Animation anim;
	public Transform target;
	private bool followBall;
	public float RotationSpeed;

	private Quaternion _lookRotation;
	private Vector3 _direction;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animation> ();
		followBall = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (true) {// followBall) {
			var dist = Vector3.Distance (transform.position, target.transform.position);
			float rate = 0.0f;
			if (dist > 3) {
				anim.CrossFade ("CorgiRun");
				rate = 0.1f;
			} else if (dist > 1) {
				anim.CrossFade ("CorgiWalk");
				rate = 0.03f;
			} else if (dist >= 0) {
				anim.CrossFade ("CorgiSitScratch");
			}
			transform.position = Vector3.MoveTowards (transform.position, target.transform.position, rate);

			_direction = (target.position - transform.position).normalized;

			//create the rotation we need to be in to look at the target
			_lookRotation = Quaternion.LookRotation(_direction);

			//rotate us over time according to speed until we are in the required rotation
			transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
		}


	}

	public void FollowBallToggleOn (){
		followBall = true;
	}
}
