using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

	protected Transform laserPoint, laserSource;
	protected Vector3 direction;
	protected Vector3 hitLocation;
	private string s_laserCannon = "LaserCannon", s_laserReflect = "LaserReflect", s_laserSensor = "LaserSensor", s_laserPortal = "LaserPortal";

	[SerializeField] protected LineRenderer lineRenderer;

	private void Awake()
	{
		LineRendSetUp ();
	}

	protected virtual void LineRendSetUp(){
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.enabled = false;
		lineRenderer.useWorldSpace = true;
	}

	protected virtual void LineRendEnable(){
		lineRenderer.enabled = true;
		lineRenderer.SetPosition (0, laserPoint.position);
		lineRenderer.SetPosition (1, hitLocation);
	}

	protected virtual void LineRendDisable(){
		lineRenderer.enabled = false;
	}

	//Vector3 heading, float distance, Vector3 direction,
	protected virtual void AdjustDirection(){
		Vector3 heading = laserPoint.position - laserSource.position;
		float distance = heading.magnitude;
		direction = heading / distance;
	}

	//Transform laserPoint, Vector3 direction
	protected virtual void ShootLaser(){
		Ray laserRay = new Ray (laserPoint.position, direction);
		RaycastHit hit;
		if (Physics.Raycast (laserRay, out hit, Mathf.Infinity))
		{
			GameObject hitObject = hit.transform.gameObject;
			if (hitObject != null)
			{
				hitLocation = hit.point;
				DisplayLaser (hit.point, hitObject);
			}
		}
	}

	protected virtual void DisplayLaser(Vector3 hitPos, GameObject hitObj){
		Debug.DrawLine (laserPoint.position, hitPos, Color.green);
		//effect
		if (gameObject.CompareTag (s_laserCannon))
		{
			if (hitObj.CompareTag (s_laserReflect))
			{
				laserReflector = hitObj;
				LaserEffect1 ();
			}
			else if (hitObj.CompareTag (s_laserPortal))
			{
				portal = hitObj;
				LaserEffect3 ();
			}
		}
		else if (gameObject.CompareTag (s_laserReflect))
		{
			if (hitObj.CompareTag (s_laserSensor))
			{
				sensor = hitObj;
				LaserEffect2 ();
			}
			else if (hitObj.CompareTag (s_laserPortal))
			{
				portal = hitObj;
				LaserEffect3 ();
			}
		}
		else if (gameObject.CompareTag (s_laserPortal))
		{
			if (hitObj.CompareTag (s_laserReflect))
			{
				laserReflector = hitObj;
				LaserEffect1 ();
			}
			else if (hitObj.CompareTag (s_laserSensor))
			{
				sensor = hitObj;
				LaserEffect2 ();
			}
			else if (hitObj.CompareTag (s_laserPortal))
			{
				portal = hitObj;
				LaserEffect3 ();
			}
		}
	}

	protected GameObject laserReflector;
	protected virtual void LaserEffect1(){
		//reflector
	}

	protected GameObject sensor;
	protected virtual void LaserEffect2(){
		//sensor
	}

	protected GameObject portal;
	protected virtual void LaserEffect3(){
		//portal
	}
}
