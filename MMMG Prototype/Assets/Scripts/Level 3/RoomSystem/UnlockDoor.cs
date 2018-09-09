using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour {
	[SerializeField] private GameObject secretDoor_col = null;
	[SerializeField] private MeshRenderer laserSensor1_MeshRend = null, laserSensor2_MeshRend = null;
	private Color lightBlue;
	private bool sensorPowered;
	private Color alpha_zero;

	private LightSwitch lightSwitch;
	[SerializeField] private SpriteRenderer secretDoor_mat = null;
	private Color alpha_increment;
	private SwitchRoom switchRoom;
	[SerializeField] private ParticleSystem ps_ground = null;

	private void Awake(){
		lightBlue = new Color (0, 0.5f, 1);
		alpha_zero = new Color (1, 1, 1, 0);
		secretDoor_mat.color = alpha_zero;
		alpha_increment = new Color (0, 0, 0, Time.deltaTime /2);
		lightSwitch = GetComponent<LightSwitch> ();
		secretDoor_col.SetActive (false);
		switchRoom = GetComponent<SwitchRoom> ();
	}

	private void Update(){
		ArrangementIndicator ();
		CheckSensor ();
		DoorUnlock ();
	}

	private void CheckSensor(){
		if (laserSensor1_MeshRend.material.color == lightBlue && laserSensor2_MeshRend.material.color == lightBlue) {
			sensorPowered = true;
		}
		else {
			sensorPowered = false;
		}
	}

	private bool correct;

	private void ArrangementIndicator(){
		if (switchRoom.isArranged && !lightSwitch.isLightOn)
		{
			if (!ps_ground.isPlaying)
			{
				correct = true;
				AudioManager.audioManager.PlaySfx (AudioManager.Sfx.arranged);
				ps_ground.Play ();
			}
		}
		else
		{
			if (ps_ground.isPlaying)
			{
				ps_ground.Stop ();
			}
			if (correct)
			{
				if (!lightSwitch.isLightOn)
				{
					AudioManager.audioManager.PlaySfx (AudioManager.Sfx.notArranged);
				}
				else
				{
					AudioManager.audioManager.PlaySfx (AudioManager.Sfx.arranged);
				}
				correct = false;
			}
		}
	}

	private void DoorUnlock(){
		if(sensorPowered && lightSwitch.isLightOn && switchRoom.isArranged && !switchRoom.isSwitching) {
			secretDoor_mat.color = secretDoor_mat.color + alpha_increment;
			if (secretDoor_mat.color == Color.white)
				secretDoor_col.SetActive (true);
			//color increase, enable collider
		} else {
			secretDoor_mat.color = alpha_zero;
			secretDoor_col.SetActive (false);
		}
	}
}
