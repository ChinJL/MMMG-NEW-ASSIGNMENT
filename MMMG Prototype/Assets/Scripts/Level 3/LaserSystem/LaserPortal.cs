using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPortal : Laser {

	private bool isPowerful;
	public float powerLvl = 0;
	private float powerThreshold = 0.25f;
	private float minPower = 0;
	private float maxPower = 0.5f;

	public Vector3 laserDirection;

	[SerializeField] private Transform portal1a = null, portal1b = null, portal2a = null, portal2b = null;
	[SerializeField] private Sprite portalSprite = null;
	private SpriteRenderer portalSpriteRend, m_spriteRend;
	[SerializeField] private Transform fakeSprite = null;
	private float rotDir;

	private void Awake(){
		DecideLaserPoint ();
		m_spriteRend = GetComponent<SpriteRenderer> ();
	}

	private void OnDisable(){
		fakeSprite.gameObject.SetActive (false);
	}

	private void Update(){
		PowerMonitor ();
		PowerfulEffect ();
		PortalAppearance ();
	}

	private void DecideLaserPoint(){
		if (transform == portal1a)
		{
			rotDir = 1;
			laserPoint = portal1b;
			portalSpriteRend = laserPoint.GetComponent<SpriteRenderer> ();
		}
		else if (transform == portal1b)
		{
			rotDir = -1;
			laserPoint = portal1a;
			portalSpriteRend = laserPoint.GetComponent<SpriteRenderer> ();
		}
		else if (transform == portal2a)
		{
			rotDir = 1;
			laserPoint = portal2b;
			portalSpriteRend = laserPoint.GetComponent<SpriteRenderer> ();
		}
		else if (transform == portal2b)
		{
			rotDir = -1;
			laserPoint = portal2a;
			portalSpriteRend = laserPoint.GetComponent<SpriteRenderer> ();
		}
	}

	private void PortalAppearance(){
		if(m_spriteRend.sprite == portalSprite)
		{
			fakeSprite.gameObject.SetActive (true);
		}
		else
		{
			LineRendDisable ();
		}

		if (fakeSprite.gameObject.activeSelf)
		{
			fakeSprite.RotateAround (fakeSprite.position, fakeSprite.forward, rotDir);
		}
	}

	protected override void ShootLaser(){
		if (laserPoint.gameObject.activeSelf && portalSpriteRend.sprite == portalSprite)
		{
			Ray laserRay = new Ray (laserPoint.position, laserDirection);
			RaycastHit hit;
			if (Physics.Raycast (laserRay, out hit, Mathf.Infinity))
			{
				GameObject hitObject = hit.transform.gameObject;
				if (hitObject != null)
				{
					hitLocation = hit.point;
					DisplayLaser (hit.point, hitObject);
					LineRendEnable ();
				}
				else
				{
					lineRenderer.enabled = false;
				}
			}
		}
	}

	protected override void LaserEffect1(){
		LaserReflect laserReflect = laserReflector.GetComponent<LaserReflect> ();
		laserReflect.powerLvl += Time.deltaTime * 2;
	}

	protected override void LaserEffect2(){
		LaserReceiver laserReceiver = sensor.GetComponent<LaserReceiver> ();
		laserReceiver.powerLvl += Time.deltaTime * 2;
	}

	protected override void LaserEffect3(){
		LaserPortal laserPortal = portal.GetComponent<LaserPortal> ();
		laserPortal.powerLvl += Time.deltaTime * 2;
		laserPortal.laserDirection = laserDirection;
	}

	private void PowerMonitor(){
		if (powerLvl > powerThreshold)
		{
			isPowerful = true;
			if (powerLvl >= maxPower)
			{
				powerLvl = maxPower;
			}
		}
		else if (powerLvl <= powerThreshold)
		{
			isPowerful = false;
		}
		if(powerLvl >= minPower)
			powerLvl -= Time.deltaTime;
	}

	private void PowerfulEffect(){
		if (isPowerful)
		{
			ShootLaser ();
			ShotSound ();
		}
		else
		{
//			lineRenderer.enabled = false;
			isShot = true;
		}
	}

	private bool isShot = true;
	private void ShotSound(){
		if (isPowerful && isShot)
		{
			AudioManager.audioManager.PlaySfx (AudioManager.Sfx.laser);
			isShot = false;
		}
	}
}
