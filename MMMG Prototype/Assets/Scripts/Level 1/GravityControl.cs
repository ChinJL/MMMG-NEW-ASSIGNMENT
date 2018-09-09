using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GravityControl : MonoBehaviour {

	Rigidbody m_rb;
	public bool gravityControl;

	public LightSwitch lightSwitch;

	public Animator anim;

	// Use this for initialization
	void Start () {
		m_rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		m_rb.AddForce (transform.up * 50);

		if (gravityControl)
		{
			m_rb.mass = 3;
		}
		else
		{
			m_rb.mass = 1200;
			m_rb.mass = 12;
		}
	}

	public void GravityController()
	{
		if (lightSwitch.isLightOn)
		{
			gravityControl = !gravityControl;
		}
	}
}
