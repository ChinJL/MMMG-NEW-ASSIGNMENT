using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

	[SerializeField] private Animator anim_inventoryPanel = null;
	[SerializeField] private int isOpen = Animator.StringToHash("isOpen");

	public void ToggleInventory(){
		if (anim_inventoryPanel.GetFloat (isOpen) == 0)
		{
			AudioManager.audioManager.PlaySfx (AudioManager.Sfx.closeBag);
			anim_inventoryPanel.SetFloat (isOpen, 1);
		}
		else
		{
			AudioManager.audioManager.PlaySfx (AudioManager.Sfx.openBag);
			anim_inventoryPanel.SetFloat (isOpen, 0);
		}
	}
}
