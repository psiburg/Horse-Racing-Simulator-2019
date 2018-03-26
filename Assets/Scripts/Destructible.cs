using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class Destructible : MonoBehaviour {
    [SerializeField] GameObject explosion;
	public AudioSource explosionSound;
	public int hitPoints = 100;
	private bool damaged;

	private void Awake() {

	}

	void Update() {

	}

	public void TakeDamage(int damage) {
		hitPoints -= damage;
		if (GetComponent<PlayerHealth>() != null) {
			Debug.Log("calling player take damage");
			GetComponent<PlayerHealth>().TakeDamage();
		}
		if (hitPoints <= 0) {
			Death();
		}
	}

	public void SpawnExplosion(Vector3 pos){
        GameObject go = Instantiate(explosion, pos, Quaternion.identity) as GameObject;
        Destroy(go, 5f);
    }

	public void Death() {

		//spawn several explosions to make a big death explosion
		for (int i = 0; i < 5; i++)
			SpawnExplosion(transform.position);

		//play explosion and delete the ship
		//explosionSound.Play();
		if (this.gameObject.CompareTag("Player")) {
			Camera.main.GetComponentInChildren<ParticleRenderer>().enabled = false;
		}
		Destroy(this.gameObject);
	}
}
