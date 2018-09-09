using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour {

	public GameObject Ball;
	public GameObject Spawner;
	private bool isBall = true;
	private GameObject m_ball;

	public void SpawnBall(){
		if (isBall)
		{
			m_ball = Instantiate (Ball, Spawner.transform.position, Spawner.transform.rotation);
			isBall = !isBall;
		}
		else
		{
			Destroy (m_ball);
			isBall = !isBall;
		}
	}
}
