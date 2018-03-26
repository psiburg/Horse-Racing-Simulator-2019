using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour {

	private Transform player;
	public RectTransform scoreboard;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		Camera.main.GetComponent<CameraScript>().enabled = false;
		scoreboard.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.transform.LookAt(player);
	}
}
