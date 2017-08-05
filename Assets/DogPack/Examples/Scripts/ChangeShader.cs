using UnityEngine;
using System.Collections;

public class ChangeShader : MonoBehaviour {

	public Material[] material;
	Renderer rend;
	int MatValue = 0;
	// Use this for initialization
	void Start () 
	{
		rend = GetComponent<Renderer> ();
		rend.enabled = true;
		rend.sharedMaterial = material [0];
	}

	// Update is called once per frame
	public void ChangeShaderButtonClicked()
	{
		print("Change mat");
		if (MatValue < material.Length-1
		) {
			MatValue++;
		}
		else MatValue = 0;
		rend.sharedMaterial = material [MatValue];
	}
}
