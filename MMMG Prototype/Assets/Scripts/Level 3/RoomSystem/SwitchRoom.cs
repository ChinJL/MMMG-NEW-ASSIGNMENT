using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchRoom : MonoBehaviour {
	[Header("Layers")]
	[Tooltip("Place rooms/layers in ascending order.")]
	[SerializeField] private Transform room1 = null;
	[Tooltip("Place rooms/layers in ascending order.")]
	[SerializeField] private Transform room2 = null, room3 = null, room4 = null, room5 = null;
	[Header("Attribute")] 
	[Tooltip("Adjust distance by layer size accordingly.")]
	[SerializeField] private float distance = 2.5f;
	[Tooltip("Adjust distance by layer size accordingly.")]
	[SerializeField] private float distanceMultiplier = 4;
	[Tooltip("Adjust speed for the process of moving layers.")]
	[SerializeField][Range(1, 10)] private float speed = 5;
	private Vector3 initialPos1, initialPos2, initialPos3, initialPos4;
	public bool isSwitching;
	private float dropDistance = 5;
	private Vector3
	up = Vector3.up,
	down = Vector3.down,
	left = Vector3.left, 
	right = Vector3.right;
	[SerializeField] private GameObject _room1 = null, _room2 = null, _room3 = null, _room4 = null, _room5 = null;
	public bool isArranged;
	[SerializeField] private GameObject[] reflectors = null;
	[SerializeField] private GameObject player = null;
	private Vector3 initialPlayerPos;
	[SerializeField] private GameObject[] portalObj = null;
	[SerializeField] private GameObject moveLeft = null, moveRight = null;

	private void CheckRoomArrangement(){
		if (room1 == _room1.transform && room2 == _room2.transform && room3 == _room3.transform && room4 == _room4.transform && room5 == _room5.transform) {
			isArranged = true;
		} else {
			isArranged = false;
		}
	}

	public void SwitchPosition_1(){
		if (!isSwitching) {
			initialPlayerPos = player.transform.position;
			isSwitching = !isSwitching;
			initialPos1 = room1.localPosition;
			initialPos2 = room2.localPosition;
			initialPos3 = room3.localPosition;
			initialPos4 = room4.localPosition;
			StartCoroutine(DelaySwitchRoom_1 ());
		} else {
			return;
		}
	}

	private IEnumerator DelaySwitchRoom_1(){
		DropReflector ();
		yield return new WaitForSeconds (1.5f);
		StartCoroutine (moveLayer (room1, up, right, initialPos3, distance, speed, distanceMultiplier));
		StartCoroutine (moveLayer (room2, up, right, initialPos4, distance, speed, distanceMultiplier));
		StartCoroutine (moveLayer (room3, down, left, initialPos1, distance, speed, distanceMultiplier));
		StartCoroutine (moveLayer (room4, down, left, initialPos2, distance, speed, distanceMultiplier));
		Transform tempRoom1 = room1;
		room1 = room3;
		room3 = tempRoom1;
		Transform tempRoom2 = room2;
		room2 = room4;
		room4 = tempRoom2;
		yield return new WaitForSeconds (2f);
		player.transform.position = initialPlayerPos;
		RaiseReflector ();
		yield return null;
	}

	public void SwitchPosition_2(){
		if (!isSwitching) {
			initialPlayerPos = player.transform.position;
			isSwitching = !isSwitching;
			initialPos1 = room2.localPosition;
			initialPos2 = room3.localPosition;
			initialPos3 = room4.localPosition;
			initialPos4 = room5.localPosition;
			StartCoroutine(DelaySwitchRoom_2 ());
		} else {
			return;
		}
	}

	private IEnumerator DelaySwitchRoom_2(){
		DropReflector ();
		yield return new WaitForSeconds (1.5f);
		StartCoroutine (moveLayer (room2, up, right, initialPos3, distance, speed, distanceMultiplier));
		StartCoroutine (moveLayer (room3, up, right, initialPos4, distance, speed, distanceMultiplier));
		StartCoroutine (moveLayer (room4, down, left, initialPos1, distance, speed, distanceMultiplier));
		StartCoroutine (moveLayer (room5, down, left, initialPos2, distance, speed, distanceMultiplier));
		Transform tempRoom2 = room2;
		room2 = room4;
		room4 = tempRoom2;
		Transform tempRoom3 = room3;
		room3 = room5;
		room5 = tempRoom3;
		yield return new WaitForSeconds (2f);
		player.transform.position = initialPlayerPos;
		RaiseReflector ();
		yield return null;
	}

	private IEnumerator moveLayer(Transform layer, Vector3 dir1, Vector3 dir2, Vector3 targetPos, float distance, float speed, float distanceMultiplier){
		//Change destination condition based on the heading direction
		Vector3 destination = layer.localPosition + dir1 * distance;
		AudioManager.audioManager.PlaySfx (AudioManager.Sfx.roomMoving);
		while (layer.localPosition.y != destination.y) {
			layer.localPosition = Vector3.MoveTowards (layer.localPosition, destination, Time.deltaTime * speed);
			yield return null;
		}
		destination = layer.localPosition + dir2 * distanceMultiplier * distance;
		AudioManager.audioManager.PlaySfx (AudioManager.Sfx.roomMoving);
		while (layer.localPosition.x != destination.x) {
			layer.localPosition = Vector3.MoveTowards (layer.localPosition, destination, Time.deltaTime * speed);
			yield return null;
		}
		destination = targetPos;
		AudioManager.audioManager.PlaySfx (AudioManager.Sfx.roomMoving);
		while (layer.localPosition.y != destination.y) {
			layer.localPosition = Vector3.MoveTowards (layer.localPosition, destination, Time.deltaTime * speed);
			yield return null;
		}
		yield return null;
	}

	private void DropReflector(){
		DisablePlayer ();
		foreach (GameObject reflector in reflectors) {
			StartCoroutine (MoveDown (reflector));
		}
		if (isArranged)
		{
			isArranged = false;
		}
	}

	private IEnumerator MoveDown(GameObject m_reflector){
		Vector3 dropPos = new Vector3(m_reflector.transform.localPosition.x, m_reflector.transform.localPosition.y - dropDistance, m_reflector.transform.localPosition.z);
		AudioManager.audioManager.PlaySfx (AudioManager.Sfx.reflectorMoving);
		while (m_reflector.transform.localPosition != dropPos) {
			m_reflector.transform.localPosition = Vector3.MoveTowards (m_reflector.transform.localPosition, dropPos, 6f * Time.deltaTime);
			yield return null;
		}
		yield return null;
	}

	private void RaiseReflector(){
		foreach (GameObject reflector in reflectors) {
			StartCoroutine (MoveUp (reflector));
		}
		CheckRoomArrangement ();
	}

	private IEnumerator MoveUp(GameObject m_reflector){
		Vector3 dropPos = new Vector3(m_reflector.transform.localPosition.x, m_reflector.transform.localPosition.y + dropDistance, m_reflector.transform.localPosition.z);
		AudioManager.audioManager.PlaySfx (AudioManager.Sfx.reflectorMoving);
		while (m_reflector.transform.localPosition != dropPos) {
			m_reflector.transform.localPosition = Vector3.MoveTowards (m_reflector.transform.localPosition, dropPos, 6f * Time.deltaTime);
			yield return null;
		}
		yield return isSwitching = false;
		foreach (GameObject portal in portalObj) {
			portal.SetActive (true);
		}
		EnablePlayer ();
		yield return null;
	}

	private void EnablePlayer(){
		moveLeft.SetActive (true);
		moveRight.SetActive (true);
	}

	private void DisablePlayer(){
		moveLeft.SetActive (false);
		moveRight.SetActive (false);
	}
}
