using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTheDoor : MonoBehaviour {

	public GameObject showDoor;
	private Animator anim;

	MeshRenderer mesh;

	// Use this for initialization
	void Start () {
		anim = showDoor.GetComponent<Animator> ();
		mesh = GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Box")
		{
			mesh.enabled = false;
			anim.SetBool ("isShowing", true);
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Box")
		{
			mesh.enabled = true;
			anim.SetBool ("isShowing", false);
		}
	}
}
