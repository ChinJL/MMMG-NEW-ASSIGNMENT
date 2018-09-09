using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Unlock : MonoBehaviour {

	public Transform doorTransform, doorColTransform;
	private SpriteRenderer secretDoor_mat;
	private Collider secretDoor_col;
	private Color alpha_increment, alpha_zero;

	void Awake(){
		secretDoor_mat = doorTransform.GetComponent<SpriteRenderer> ();
		secretDoor_col = doorColTransform.GetComponent<Collider> ();
		alpha_increment = new Color (0, 0, 0, Time.deltaTime /2);
		alpha_zero = new Color (1, 1, 1, 0);
	}
	
	void OnTriggerStay(Collider other){
		if (other.tag == "True")
		{
			secretDoor_mat.color = secretDoor_mat.color + alpha_increment;
			if (secretDoor_mat.color == Color.white)
				secretDoor_col.gameObject.SetActive (true);
		}
		else
		{
			secretDoor_mat.color = alpha_zero;
			secretDoor_col.gameObject.SetActive (false);
		}
	}

}
