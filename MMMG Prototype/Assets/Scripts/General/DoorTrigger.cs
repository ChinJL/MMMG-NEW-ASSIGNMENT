using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour {
	private string plyer = "Player";
	private Collider m_col;

	private void Awake(){
		m_col = GetComponent<Collider> ();
	}

	private void OnEnable(){
		AudioManager.audioManager.PlaySfx (AudioManager.Sfx.correct);
	}

	private void OnTriggerEnter(Collider col){
		if (col.CompareTag (plyer)) {
			AudioManager.audioManager.PlaySfx (AudioManager.Sfx.victory);
			m_col.enabled = false;
			if (SceneManager.GetActiveScene ().buildIndex != 3)
			{
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
			}
			else
			{
				SceneManager.LoadScene (0);
			}
		}
	}
}
