using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReflect : Laser {

	private bool isPowerful;
	public float powerLvl = 0;
	private float powerThreshold = 1;
	private float minPower = 0;
	private float maxPower = 2;
	[SerializeField] Transform m_laserPoint = null;
	private Vector3 reflectOffset;
	[SerializeField] private float offset_x = 0.5f;

	public GameObject laserDestination;

	private void OnEnable(){
		laserPoint = m_laserPoint;
		laserSource = gameObject.transform;
		AdjustDirection ();
	}

	private void Update(){
		PowerMonitor ();
		PowerfulEffect ();
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
			LineRendEnable ();
		}
		else
		{
			LineRendDisable ();
			isShot = true;
		}
	}

	private bool isShot = true;
	private void ShotSound(){
		if (isPowerful && isShot)
		{
			AudioManager.audioManager.PlaySfx (AudioManager.Sfx.charged);
			isShot = false;
		}
	}

	protected override void AdjustDirection(){
		reflectOffset = new Vector3 (gameObject.transform.position.x + offset_x, gameObject.transform.position.y, gameObject.transform.position.z);
		Vector3 heading = laserPoint.position - reflectOffset;
		float distance = heading.magnitude;
		direction = heading / distance;
	}

	protected override void LaserEffect2(){
		LaserReceiver laserReceiver = laserDestination.GetComponent<LaserReceiver> ();
		laserReceiver.powerLvl += Time.deltaTime * 2;
	}

	protected override void LaserEffect3(){
		LaserPortal laserPortal = portal.GetComponent<LaserPortal> ();
		laserPortal.powerLvl += Time.deltaTime * 2;
		laserPortal.laserDirection = direction;
	}
}
