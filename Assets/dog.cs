using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class dog : MonoBehaviour {

    private float IDLE_ACTIVE_TIME = 10;

	private Rigidbody rb;
	private Animation anim;

    private float idleTimeLeft;

	private string[] walkAnimations = new string[] {
		"CorgiRun",
        "CorgiWalk",
        "CorgiGallop"
    };

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animation> ();

        idleTimeLeft = IDLE_ACTIVE_TIME;

    }

	// Update is called once per frame
	void Update () {

        idleTimeLeft -= Time.deltaTime;
        if(idleTimeLeft <= 0)
        {
            idleWalk();
        }

	}

	void startRecording() {

        anim.Play("CorgiIdle");

	}

    void idleWalk()
    {
        System.Random rnd = new System.Random();
        anim.Play(walkAnimations[rnd.Next(0, walkAnimations.Length)]);
        rb.AddRelativeForce(0, 0, 1);
    }



}