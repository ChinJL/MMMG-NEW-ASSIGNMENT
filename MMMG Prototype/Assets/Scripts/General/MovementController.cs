using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
	[SerializeField] SpriteRenderer m_spriteRend = null;
	Rigidbody m_rb;
	private float hAxis;
	[SerializeField] private float speed = 5;
	private float m_speed;
	private Vector3 hMovement;
	private bool isLeft, isRight;
	[SerializeField] private Animator anim = null;
	private int isMoving = Animator.StringToHash("isMoving");
	public bool facingLeft;
	[SerializeField] private GameObject portalGun_left = null, portalGun_right = null;
	private bool gunExist;

	private void OnEnable(){
		m_rb = GetComponent<Rigidbody>();
		if (portalGun_left != null && portalGun_right != null)
		{
			gunExist = true;
		}
	}

	private void Update(){
		DecideDirection ();
		CalculateMovement ();

		if (Input.GetKeyDown (KeyCode.A))
		{
			MoveLeft ();
		}
		if (Input.GetKeyDown (KeyCode.D))
		{
			MoveRight ();
		}
		if (Input.GetKeyUp (KeyCode.A))
		{
			ResetMovement ();
		}
		if (Input.GetKeyUp (KeyCode.D))
		{
			ResetMovement ();
		}
	}

	private void FixedUpdate(){
		MoveRb ();
	}

	public void MoveLeft(){
		isLeft = true;
		FlipLeft ();
		anim.SetFloat (isMoving, 1);
		facingLeft = true;
		if (facingLeft && gunExist)
		{
			portalGun_left.SetActive (true);
			portalGun_right.SetActive (false);
		}
	}

	public void ResetMovement(){
		isLeft = false;
		isRight = false;
		anim.SetFloat (isMoving, 0);
		if (AudioManager.audioManager.walkingSoundSource.isPlaying)
		{
			AudioManager.audioManager.walkingSoundSource.Stop();
		}
	}

	public void MoveRight(){
		isRight = true;
		FlipRight ();
		anim.SetFloat (isMoving, 1);
		facingLeft = false;
		if (!facingLeft && gunExist)
		{
			portalGun_left.SetActive (false);
			portalGun_right.SetActive (true);
		}
	}

	private void DecideDirection(){
		if (isLeft)
		{
			hAxis = -1;
			AudioManager.audioManager.PlaySfx (AudioManager.Sfx.walk);
		}
		else if (isRight)
		{
			AudioManager.audioManager.PlaySfx (AudioManager.Sfx.walk);
			hAxis = 1;
		}
		else
		{
			hAxis = 0;
		}
	}

	private void CalculateMovement(){
		m_speed = speed * Time.deltaTime;
		hMovement = new Vector3 (hAxis, 0, 0) * m_speed;
	}

	private void MoveRb(){
		m_rb.MovePosition (transform.position + hMovement);
	}

	private void FlipLeft(){
		m_spriteRend.flipX = true;
	}
	private void FlipRight(){
		m_spriteRend.flipX = false;
	}
}
