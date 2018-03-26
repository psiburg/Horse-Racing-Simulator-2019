using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour {

	public Transform target;
	public Transform player;
	private Transform oldTarget;
	public float defaultSpeed = 25f;
	public float currentSpeed;
	public float turnSpeed = 1f;
	public float evasiveSpeed = 35f;
	private bool evasive = false;
	private Text debugText;
	private int infProtection; //break infinite loops

	// Use this for initialization
	void Start () {
		currentSpeed = defaultSpeed;
		player = GameObject.FindGameObjectWithTag("Player").transform;
		target = player;
		debugText = GameObject.FindGameObjectWithTag("Debug").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		SetMoveState();
		Debug.Log(evasive);
		debugText.text = Vector3.Distance(target.position, this.transform.position).ToString() + 
			"\nev: " + evasive + 
			"\ntarpos: " + target.position + 
			"\npl dist: " + Vector3.Distance(this.transform.position, player.position);
		Turn();
		Move();
		if (Vector3.Distance(target.position, this.transform.position) > 300 && target.CompareTag("Player"))
			currentSpeed = 100f;
		else if (evasive)
			currentSpeed = evasiveSpeed;
		else
			currentSpeed = defaultSpeed;
	}

	void SetMoveState() {
		if (Vector3.Distance(player.position, this.transform.position) < 50 && !evasive) {
			print("too close to player, evading");
			evasive = true;
			FleePlayer();
		}
		else if (evasive && Vector3.Distance(target.position, this.transform.position) < 15) {

			//if player is more than 150 units away, end evasion
			if (Vector3.Distance(this.transform.position, player.position) > 100) {
				evasive = false;
				GameObject.Destroy(target.gameObject);
				target = player;
			}
			else { //pick a new target and keep evading
				GameObject.Destroy(target.gameObject);
				FleePlayer();
			}


		}
	}

	void FleePlayer() {
		target = new GameObject("evasion wp").transform;
		target.position = this.transform.position;

		//place target in a random place on x,y,z about 50 to 100 units away from the ship
		float xx = Random.Range(25,50);
		if (Random.Range(0, 1) == 0)
			xx = -xx;
		float yy = Random.Range(25,50);
		if (Random.Range(0, 1) == 0)
			yy = -yy;
		float zz = Random.Range(25,50);
		if (Random.Range(0, 1) == 0)
			zz = -zz;
		target.Translate(xx,yy,zz);
	}
	void Turn() {
		Vector3 pos = target.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(pos);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
	}

	void Move() {
		transform.position += transform.forward * Time.deltaTime * currentSpeed;
	}
}
