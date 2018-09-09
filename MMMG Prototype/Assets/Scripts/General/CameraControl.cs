using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public Transform player;

	public Vector3 offset;

	void Update()
	{
		transform.position = new Vector3 (player.position.x, 0, 0) + offset;
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -1.5f, 12), 1, -10);
	}

	// Update is called once per frame
//	void LateUpdate () {
//		transform.position = player.transform.position + offSet;
//	}
}
