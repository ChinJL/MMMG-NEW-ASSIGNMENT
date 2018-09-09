using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class PortalGun : MonoBehaviour {
	[SerializeField] private Transform player = null;
	private float projectile_distance = 15f;
	[SerializeField] private Transform source = null;
	private Vector3 heading;
	private float distance;
	private Vector3 direction;
	private bool isAim;
	private Transform surface;
	[SerializeField] private GameObject[] portalObj = null;
	private int turn = 0;
	[SerializeField] private float bulletSpeed = 10f;
	[SerializeField] private float offset_y = 5, offset_z = 0.75f;
	private bool isReached;
	[SerializeField] private GameObject roomLayer = null;
	SwitchRoom switchRoom;
	[SerializeField] private KeyCode aimKey = KeyCode.None, shootKey = KeyCode.None, resetKey = KeyCode.None;
	[SerializeField] private LineRenderer lineRend = null;

	public LayerMask notPlayerMask;
	MovementController moveCtrl;

	[SerializeField] private Sprite bulletSprite = null, portalSprite = null;
	[SerializeField] private List<ParticleSystem> ps_bullet = new List<ParticleSystem>();
	[SerializeField] private Transform portalGun1 = null, portalGun2 = null;
	PortalGun anotherPortalGun;

	private void Start()
	{
		lineRend = GetComponent<LineRenderer> ();
		lineRend.enabled = false;
		lineRend.useWorldSpace = true;
	}

	private void Awake(){
		if (transform == portalGun1)
		{
			anotherPortalGun = portalGun2.GetComponent<PortalGun> ();
		}
		else if (transform == portalGun2)
		{
			anotherPortalGun = portalGun1.GetComponent<PortalGun> ();
		}
	}

	private void OnEnable(){
		isAim = false;
		isReached = true;
		switchRoom = roomLayer.GetComponent<SwitchRoom> ();
		moveCtrl = player.GetComponent<MovementController> ();
	}

	private void Update(){
		MeasureDirection (source, player);
		ToggleShoot ();
		if (isAim) {
			Aim ();
			lineRend.enabled = true;
		}else{
			lineRend.enabled = false;
		}

		if (switchRoom.isSwitching) {
			foreach (GameObject portal in portalObj) {
				portal.SetActive (false);
			}
		}
	}

	private void ToggleShoot(){
		if (Input.GetKeyDown (aimKey))
		{
			anotherPortalGun.isAim = false;
			isAim = !isAim;
		}
	}

	public void ToggleAim(){
			anotherPortalGun.isAim = false;
			isAim = !isAim;
	}

	public void OffAim(){
		isAim = false;
		anotherPortalGun.isAim = false;
	}

	private void MeasureDirection(Transform source, Transform player){
		if (moveCtrl.facingLeft)
		{
			source.localPosition = new Vector3 (-.5f, .55f, 0);
			heading = source.position - player.position - new Vector3 (-0.35f, 0, 0f);
			distance = heading.magnitude;
			direction = heading / distance;
		}
		else if (!moveCtrl.facingLeft)
		{
			source.localPosition = new Vector3 (.93f, .55f, 0);
			heading = source.position - player.position - new Vector3 (0.75f, 0, 0f);
			distance = heading.magnitude;
			direction = heading / distance;
		}
	}

	private void Aim(){
		Ray ray = new Ray (source.position, direction);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, projectile_distance, notPlayerMask)) {
			Debug.DrawLine (source.position, hit.point, Color.red);
			//reminder: set line rend setting to use world space position, wasted hours for using local position - -
			lineRend.SetPosition (0, source.position);
			lineRend.SetPosition (1, hit.point);
			Shoot (hit.point);
		}
	}

	public void ShootPortal(){
		buttonShoot = true;
	}
		
	private bool buttonShoot;
	private void Shoot(Vector3 location){
		if (Input.GetKeyDown (shootKey) && isReached || buttonShoot && isReached)
		{	
			buttonShoot = false;
			isReached = false;
			Vector3 aimedPos = location;
			aimedPos = aimedPos + (Vector3.down * offset_y) + (Vector3.forward * offset_z);
			if (turn == 2)
			{
				ResetPortal ();
			}
			else
			{
				portalObj [turn].SetActive (true);
				StartCoroutine (PlacePortal (portalObj [turn], aimedPos));
				turn++;
			}
		}

		if (Input.GetKeyDown (resetKey) && isReached) {
			ResetPortal ();
//			portal_1.lineRenderer.enabled = false;
//			portal_2.lineRenderer.enabled = false;
		}
	}

	private void ResetPortal(){
		foreach (GameObject portal in portalObj) {
			portal.SetActive (false);
		}
		isReached = true;
		turn = 0;
//		AudioManager.audioManager.PlaySfx (AudioManager.Sfx.onLight); ***
//		GameObject[] laserGun = GameObject.FindGameObjectsWithTag ("LaserGun");
//		foreach (GameObject gun in laserGun)
//		{
//			LaserGunToReflector portal = gun.GetComponent<LaserGunToReflector> ();
//			portal.hit_Portal_1 = false;
//			portal.hit_Portal_2 = false;
//		}
//		GameObject[] reflector = GameObject.FindGameObjectsWithTag ("Reflector");
//		foreach (GameObject reflect in reflector) {
//			LaserReflectorToSensor portalHit = reflect.GetComponent<LaserReflectorToSensor> ();
//			portalHit.hittedPortal_1 = false;
//			portalHit.hittedPortal_2 = false;
//		}
	}

	private IEnumerator PlacePortal(GameObject bullet, Vector3 targetLocation){
		SpriteRenderer bulletSpriteRend = bullet.GetComponent<SpriteRenderer> ();
		bulletSpriteRend.sprite = bulletSprite;
		bullet.transform.position = source.position;
		PlayBulletEffect (bullet.transform);
		AudioManager.audioManager.PlaySfx (AudioManager.Sfx.portalBullet);
		while(bullet.transform.position!= targetLocation){
			bullet.transform.position = Vector3.MoveTowards (bullet.transform.position, targetLocation, bulletSpeed * Time.deltaTime);
			yield return null;
		}
		StopBulletEffect (ps_bullet [count]);
		bulletSpriteRend.sprite = portalSprite;
		yield return isReached = true;
		yield return null;
	}

	private int count;

	private void PlayBulletEffect(Transform bullet){
		for (int i = 0; i < ps_bullet.Count; i++)
		{
			if (!ps_bullet [i].isPlaying)
			{
				ps_bullet [i].transform.SetParent (bullet);
				ps_bullet [i].transform.position = bullet.position;
				if (!ps_bullet [i].isPlaying)
					ps_bullet [i].Play ();
				count = i;
				break;
			}
		}
	}

	private void StopBulletEffect(ParticleSystem ps_tempBullet){
		if (ps_tempBullet.isPlaying)
		{
			ps_tempBullet.Stop ();
		}
	}
}
