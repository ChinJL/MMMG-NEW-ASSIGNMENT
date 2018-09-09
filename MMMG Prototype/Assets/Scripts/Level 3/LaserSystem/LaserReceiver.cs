using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReceiver : MonoBehaviour {

	public bool isPowerful;
	public float powerLvl = 0;
	private float powerThreshold = 1;
	private float minPower = 0;
	private float maxPower = 2;
	private MeshRenderer meshRend;
	private Color lightBlue;

	private void Awake(){
		meshRend = GetComponent<MeshRenderer> ();
		meshRend.material.color = Color.black;
		lightBlue = new Color (0, 0.5f, 1);
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
			PowerUp ();
			PoweredSound();
		}
		else
		{
			PowerDown ();
			isPowered = true;
		}
	}

	private void PowerUp(){
		meshRend.material.color = lightBlue;
		//play particle effect
	}

	private void PowerDown(){
		meshRend.material.color = Color.black;
	}

	private bool isPowered = true;
	private void PoweredSound(){
		if (isPowerful && isPowered)
		{
			AudioManager.audioManager.PlaySfx (AudioManager.Sfx.charged);
			isPowered = false;
		}
	}
}
