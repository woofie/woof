using UnityEngine;
using System.Collections;

public class ChangeBlendShape : MonoBehaviour {

	int blendShapeCount;
	SkinnedMeshRenderer skinnedMeshRenderer;
	Mesh skinnedMesh;
	float blendCounter = 0f;
	public float blendSpeed = 10f;
	bool blendFinished = true;
	int CurrentBlend = 0;
	float PreviosBlendCounter = 0f;

	void Awake ()
	{
		skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();
		skinnedMesh = GetComponent<SkinnedMeshRenderer> ().sharedMesh;
	}

	void Start () 
	{
		blendShapeCount = skinnedMesh.blendShapeCount; 
	}
	public void ChangeBlend()
	{
		blendCounter = 0;
		blendFinished = false;
		PreviosBlendCounter = skinnedMeshRenderer.GetBlendShapeWeight (CurrentBlend);
		skinnedMeshRenderer.SetBlendShapeWeight (CurrentBlend, 0f);
		if (CurrentBlend < blendShapeCount)
			CurrentBlend++;
		else
			CurrentBlend = 0;
		
		
	}
	void Update ()
	{
		if (!blendFinished) {
			if (PreviosBlendCounter > 0) 
			{
				PreviosBlendCounter -= blendSpeed;
				skinnedMeshRenderer.SetBlendShapeWeight (CurrentBlend-1, PreviosBlendCounter);
			}
			if (blendCounter < 100f) {
				skinnedMeshRenderer.SetBlendShapeWeight (CurrentBlend, blendCounter);
				blendCounter += blendSpeed;
			} else {
				blendFinished = true;
			}
		}
	}
}
