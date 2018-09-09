using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerLevel1 : MonoBehaviour {

	public GameObject doorTrigger;

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == ("Player"))
		{
			Destroy (gameObject);
			doorTrigger.SetActive (true);
		}
	}
}
