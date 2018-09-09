using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFloating : MonoBehaviour {

	Rigidbody m_rb;

	// Use this for initialization
	void Start () {
		m_rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		m_rb.AddForce (transform.forward * 50);
	}
}
