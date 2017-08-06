using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class dog : MonoBehaviour {

    private float IDLE_ACTIVE_TIME = 10;

	private Rigidbody rb;
	private Animation anim;
	public GameObject target;
	private bool followBall;
	public float RotationSpeed;

    private float idleTimeLeft;

    private System.Random globalRnd;

    private Vector3 initTargetPosition;


    private string[] walkAnimations = new string[] {
		"CorgiRun",
        "CorgiWalk",
        "CorgiGallop"
    };

	private Quaternion _lookRotation;
	private Vector3 _direction;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animation> ();
	
		followBall = false;
        target.SetActive(false);

        idleTimeLeft = IDLE_ACTIVE_TIME;

        globalRnd = new System.Random();

        initTargetPosition = target.transform.position;
    }

	// Update is called once per frame
	void Update () {

        if (followBall) {
            var dist = Vector3.Distance(transform.position, target.transform.position);
            float rate = 0.0f;
            if (dist > 3) {
                anim.CrossFade("CorgiRun");
                rate = 0.1f;
            } else if (dist > 1) {
                anim.CrossFade("CorgiWalk");
                rate = 0.03f;
            } else if (dist >= 0) {
                anim.CrossFade("CorgiSitScratch");
            }
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, rate);

            _direction = (target.transform.position - transform.position).normalized;

            //create the rotation we need to be in to look at the target
            _lookRotation = Quaternion.LookRotation(_direction);

			//rotate us over time according to speed until we are in the required rotation
			transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
        }

        if (idleTimeLeft != -1)
        {
            idleTimeLeft -= Time.deltaTime;
            if (idleTimeLeft <= 0)
            {
                enterIdle();
            }
        }
    }

	void startRecording() {        
		anim.Play("CorgiIdle");
	}

    void enterIdle() {

        idleTimeLeft = -1;

        if (globalRnd.Next(0,2) == 0)
        {
            idleWalk();
        }
        else
        {
            FollowBallToggleOn();
        }
    }

    private void FixedUpdate()
    {
    }

    void exitIdle()
    {
        idleTimeLeft = IDLE_ACTIVE_TIME;
        target.SetActive(false);
    }

    void idleWalk()
    {
        idleTimeLeft = -1;

        target.transform.position = initTargetPosition;

        anim.Play(walkAnimations[globalRnd.Next(0, walkAnimations.Length)]);
        rb.AddRelativeForce(0, 0, 1);
    }

	public void FollowBallToggleOn (){
		followBall = true;
        target.SetActive(true);
	}
}
