using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAttack : MonoBehaviour {

	public Transform target;
	public Laser[] laser;

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (InFront()) {
			foreach (Laser l in laser) {
				l.FireLaser(transform.position + (transform.forward * l.Distance));
				//here is where the enemy spawns a projectile 
				//GameObject laserBeam = new GameObject("Capsule");
			}
		}



	}

	bool InFront() {
		Vector3 dirToTarget = transform.position - target.position;
		float angle = Vector3.Angle(transform.forward, dirToTarget);

		if (Mathf.Abs(angle) > 170 && Mathf.Abs(angle) < 190) {
			//Debug.DrawLine(transform.position, target.position, Color.red);
			return true;
		}

		//Debug.DrawLine(transform.position, target.position, Color.green);
		return false;
	}

	//bool LineOfSight() {
	//	RaycastHit hit;

	//	Debug.DrawRay
	//}
}
