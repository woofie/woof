using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ChangeButtonText : MonoBehaviour {


	public int ValuetoGet = 0;
	//public AnimationClip[] animations;
	public string[] AnimationNames;

	public string AnimationSelected;
	void Start()
	{
		AnimationSelected = AnimationNames[0];
	}


	public void ChangeText()
	{
		GetComponentInChildren<Text>().text = AnimationNames[ValuetoGet];
		if (ValuetoGet < AnimationNames.Length-1) {
			ValuetoGet++;
		}
		else ValuetoGet = 0;
	}
}
