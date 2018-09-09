using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenPathTrigger : MonoBehaviour {

	public GameObject hiddenPathAnimator;
	private Animator anim;

	public MeshRenderer mesh;

	public bool isShowing = true;

	// Use this for initialization
	void Start () {
		anim = hiddenPathAnimator.GetComponent<Animator> ();
		mesh = GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Box")
		{
			isShowing = false;

			anim.SetBool ("isOpenning", true);
			if (!isShowing)
			{
				mesh.enabled = false;
			}
		}
	}

	void OnTriggerExit (Collider col)
	{
		if (col.tag == "Box")
		{
			isShowing = false;

			anim.SetBool ("isOpenning", false);
			if (!isShowing)
			{
				mesh.enabled = true;

				isShowing = true;
			}
		}
	}
}
