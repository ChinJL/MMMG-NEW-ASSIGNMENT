using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LightSwitch : MonoBehaviour {
	//[SerializeField] private SpriteRenderer[] graffiti_spriteRend = null;
	//private Material[] graffiti_mat;
	[SerializeField] private GameObject directionalLight = null;
	[SerializeField] private SpriteRenderer[] walls = null;
	private Sprite[] wallSprite;
	public bool isLightOn;
	[SerializeField] private Material graffiti_mat = null;
	private Color alpha_zero, alpha_one, alpha_increment, alpha_decrement, white, grey;
	//[SerializeField] private bool isLvl1 = false;

	private void Awake(){
		directionalLight.SetActive (true);
		isLightOn = true;
		alpha_one = Color.white;
		alpha_zero = new Color (1, 1, 1, 0);
		alpha_increment = new Color (0, 0, 0, Time.deltaTime);
		alpha_decrement = new Color (0, 0, 0, -Time.deltaTime);
		white = Color.white;
		grey = Color.gray;
		graffiti_mat.color = alpha_zero;
		foreach (SpriteRenderer wall in walls) {
			wall.color = white;
		}
	}

	public void ToggleLight(){
		isLightOn = !isLightOn;
		directionalLight.SetActive (isLightOn);
		if (isLightOn)
		{
			AudioManager.audioManager.PlaySfx (AudioManager.Sfx.offLight);
			foreach (SpriteRenderer wall in walls)
			{
				wall.color = white;
			}
			StartCoroutine (SwitchOnLight ());

			//if (isLvl1)
			//{
				EnableHiddenPathTrigger ();
			//}
		}
		else
		{
			AudioManager.audioManager.PlaySfx (AudioManager.Sfx.onLight);
			foreach (SpriteRenderer wall in walls)
			{
				wall.color = grey;
			}
			StartCoroutine (SwitchOffLight ());

			//if (isLvl1)
			//{
				DisableHiddenPathTrigger ();
			//}
		}
	}



	private IEnumerator SwitchOnLight(){
		while (graffiti_mat.color.a > 0 && isLightOn) {
			graffiti_mat.color = graffiti_mat.color + alpha_decrement;
			yield return null;
		}
		yield return graffiti_mat.color = alpha_zero;
	}

	private IEnumerator SwitchOffLight(){
		while (graffiti_mat.color.a < 1 && !isLightOn) {
			graffiti_mat.color = graffiti_mat.color + alpha_increment;
			yield return null;
		}
		yield return graffiti_mat.color = alpha_one;
	}

	private void EnableHiddenPathTrigger(){
		GameObject[] HiddenPath = GameObject.FindGameObjectsWithTag ("HiddenPathTrigger");
		foreach (GameObject HP in HiddenPath)
		{
			HiddenPathTrigger hiddenPath = HP.GetComponent<HiddenPathTrigger> ();
			if (hiddenPath.isShowing)
			{
				hiddenPath.mesh.enabled = false;
			}
		}
	}

	private void DisableHiddenPathTrigger(){
		GameObject[] HiddenPath = GameObject.FindGameObjectsWithTag ("HiddenPathTrigger");
		foreach (GameObject HP in HiddenPath)
		{
			HiddenPathTrigger hiddenPath = HP.GetComponent<HiddenPathTrigger> ();
			if (hiddenPath.isShowing)
			{
				hiddenPath.mesh.enabled = true;
			}
		}
	}

	/*
	private void Awake(){
		directionalLight.SetActive (true);
		isLightOn = true;
		graffiti_mat = new Material[graffiti_spriteRend.Length];
		for (int i = 0; i < graffiti_spriteRend.Length; i++){
			graffiti_mat [i] = graffiti_spriteRend [i].material;
		}
	}

	public void ToggleLight(){
		isLightOn = !isLightOn;
		directionalLight.SetActive (isLightOn);
		if (isLightOn) {
			StartCoroutine (SwitchOnLight());
		} else {
			StartCoroutine (SwitchOffLight());
		}
	}

	private IEnumerator SwitchOnLight(){
		float colorValue = 1;
		while (colorValue > 0 && isLightOn) {
			foreach (Material mat in graffiti_mat) {
				mat.color = mat.color - new Color (0, 0, 0, 0.1f);
				colorValue = mat.color.a;
				yield return null;
			}
		}
		yield return null;
	}

	private IEnumerator SwitchOffLight(){
		print ("hello");
		float colorValue = 0;
		while (colorValue < 1 && !isLightOn) {
			foreach (Material mat in graffiti_mat) {
				mat.color = mat.color + new Color (0, 0, 0, 0.1f);
				colorValue = mat.color.a;
				yield return null;
			}
		}
		yield return null;
	}
	*/
}
