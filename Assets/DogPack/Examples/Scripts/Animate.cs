using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Animate : MonoBehaviour {

	private Animator anim;
	private string AnimatorName;
	private int Move;
	int Pose = 0;
	int CurrentPose = 0;
	bool ChangePose = false;
	public bool StateChangeComplete = true;
	//public GameObject target;
	private string CurrentButtonPressed = "Stand";

	private GameObject AggressiveButton;
	private GameObject LayButton;
	private GameObject StandButton;
	private GameObject SitButton;
	private GameObject ConsumeButton;

	private float CrossfadeVal = 0.25f;
	void Start () 
	{
		AggressiveButton = GameObject.Find("Aggressive");
		LayButton = GameObject.Find("Lay");
		StandButton = GameObject.Find("Stand");
		SitButton = GameObject.Find("Sit");
		ConsumeButton = GameObject.Find("Consume");

		anim = GetComponent<Animator> ();
		AnimatorName = anim.name;
		print ("name " + AnimatorName);
	}

	void Update () 
	{
		if (ChangePose) 
		{
			print ("Change Pose");
			ChangePose = false;
			//if stands
			if (CurrentPose == 0) {
				if (Pose == 1) {
					anim.CrossFade (AnimatorName+"IdleToAggressive", CrossfadeVal);
				} else if (Pose == 2) {
					anim.CrossFade (AnimatorName +"IdleToSit", CrossfadeVal);
				} else if (Pose == 3) {
					anim.CrossFade (AnimatorName + "IdleToLay", CrossfadeVal);
				} 
				else if (Pose == 5) {
					anim.CrossFade (AnimatorName + "IdleToConsume", CrossfadeVal);
				} 
				CurrentPose = Pose;
			}
			//aggressive
			else if (CurrentPose == 1) {
				if (Pose == 0) {
					anim.CrossFade (AnimatorName+"AggressiveToIdle", CrossfadeVal);
				} else if (Pose == 2) {
					anim.CrossFade (AnimatorName+"AggressiveToSitTrans", CrossfadeVal);
				} else if (Pose == 3) {
					anim.CrossFade (AnimatorName+"AggressiveToLayTrans", CrossfadeVal);
				} else if (Pose == 4) {
					anim.CrossFade (AnimatorName + "AggressiveToIdle", CrossfadeVal);
				}
				else if (Pose == 5) {
					anim.CrossFade (AnimatorName + "AggressiveToEat", CrossfadeVal);
				} 
				CurrentPose = Pose;
			}
			//Sit
			else if (CurrentPose == 2) {
				if (Pose == 0) {
					anim.CrossFade (AnimatorName + "SitToIdle", CrossfadeVal);
				} else if (Pose == 1) {
					anim.CrossFade (AnimatorName + "SitToAggressiveTrans", CrossfadeVal);
				} else if (Pose == 3) {
					anim.CrossFade (AnimatorName + "SitToLay", CrossfadeVal);
				} else if (Pose == 4) {
					anim.CrossFade (AnimatorName + "SitToIdle", CrossfadeVal);
				}
				else if (Pose == 5) {
					anim.CrossFade (AnimatorName + "SitToEat", CrossfadeVal);
				} 
				CurrentPose = Pose;
			}
			//Lay
			else if (CurrentPose == 3) {
				if (Pose == 0) {
					anim.CrossFade (AnimatorName + "LayToIdle", CrossfadeVal);
				} else if (Pose == 1) {
					anim.CrossFade (AnimatorName + "LayToAggressiveTrans", CrossfadeVal);
				} else if (Pose == 2) {
					anim.CrossFade (AnimatorName + "LayToSit", CrossfadeVal);
				} else if (Pose == 4) {
					anim.CrossFade (AnimatorName + "LayToIdle", CrossfadeVal);
				}
				else if (Pose == 5) {
					anim.CrossFade (AnimatorName + "LayToEat", CrossfadeVal);
				} 
				CurrentPose = Pose;
			}
			//walk or consume
			else if (CurrentPose == 4 || CurrentPose == 5) {
				if (Pose == 0) {
					anim.CrossFade (AnimatorName + "Idle", CrossfadeVal);
				} else if (Pose == 1) {
					anim.CrossFade (AnimatorName + "IdleToAggressive", CrossfadeVal);
				} else if (Pose == 2) {
					anim.CrossFade (AnimatorName + "IdleToSit", CrossfadeVal);
				} else if (Pose == 3) {
					anim.CrossFade (AnimatorName + "IdleToLay", CrossfadeVal);
				}
				else if (Pose == 5) {
					anim.CrossFade (AnimatorName + "IdleToConsume", CrossfadeVal);
				}
				CurrentPose = Pose;
			} 
		}
	}
	public void StandButtonClicked()
	{
		if (CurrentButtonPressed != "Stand") {
			Pose = 0;
			ChangePose = true;
			ResetButtonNames ();
		}else {
			anim.CrossFade (AnimatorName + StandButton.GetComponentInChildren<Text> ().text, 0.5f);
		}
		Move = 0;
		anim.SetFloat ("Move", Move);
		CurrentButtonPressed = "Stand";
	}
	public void SitButtonClicked()
	{
		if (CurrentButtonPressed != "Sit") {
			Pose = 2;
			ChangePose = true;
			ResetButtonNames ();
		}else {
			anim.CrossFade (AnimatorName + SitButton.GetComponentInChildren<Text> ().text, 0.5f);
		}
		Move = 0;
		CurrentButtonPressed = "Sit";
		anim.SetFloat ("Move", Move);
	}
	public void LayButtonClicked()
	{
		if (CurrentButtonPressed != "Lay") {
			Pose = 3;
			ChangePose = true;
			ResetButtonNames ();
		}else {
			anim.CrossFade (AnimatorName + LayButton.GetComponentInChildren<Text> ().text, 0.5f);
		}


		Move = 0;
		anim.SetFloat ("Move", Move);
		CurrentButtonPressed = "Lay";
	}
	public void ConsumeButtonClicked()
	{
		if (CurrentButtonPressed != "Consume") 
		{
			Pose = 5;
			ChangePose = true;
			ResetButtonNames ();
		} else {
			anim.CrossFade (AnimatorName + ConsumeButton.GetComponentInChildren<Text> ().text, 0.5f);
		}
		Move = 0;
		anim.SetFloat ("Move", Move);
		CurrentButtonPressed = "Consume";
	}
	public void AggressiveButtonClicked ()
	{
		if (CurrentButtonPressed != "Aggressive") 
		{
			Pose = 1;
			ChangePose = true;
			ResetButtonNames ();
		} else {
			anim.CrossFade (AnimatorName + AggressiveButton.GetComponentInChildren<Text> ().text, 0.5f);
		}
		Move = 0;
		anim.SetFloat ("Move", Move);
		CurrentButtonPressed = "Aggressive";
	}
	bool BackWards =false;
	public void WalkButtonClicked()
	{
		if (Move < 5 && !BackWards) 
		{
			Move++;
		}
		else 
		{
			BackWards = true;
			Move--;
			if (Move == 1) {
				BackWards = false;
			}
		}
		anim.SetFloat ("Move", Move);

		if (Pose != 4) {
			ChangePose = true;
			ResetButtonNames ();
		}
		Pose = 4;
		CurrentButtonPressed = "Walk";
	}
	void ResetButtonNames()
	{
		GameObject ButtonToReset = GameObject.Find(CurrentButtonPressed);
		ButtonToReset.GetComponentInChildren<Text> ().text = CurrentButtonPressed;
		print ("change button name and it is now " + ButtonToReset.GetComponentInChildren<Text> ().text);
		ButtonToReset.GetComponentInChildren<ChangeButtonText> ().ValuetoGet = 0;
	}
}
