using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour {

	private Transform player;
	public GameObject scoreboard;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		Camera.main.GetComponent<CameraScript>().enabled = false;
		Camera.main.gameObject.transform.rotation = player.rotation;
		Camera.main.gameObject.transform.Translate(new Vector3(100, 100, 100));
		player.GetComponent<PlayerControl>().enabled = false;
		player.GetComponent<PlayerInput>().enabled = false;
		player.gameObject.AddComponent<AutoForward>();
		scoreboard.SetActive(true);
		GameObject ui = GameObject.FindGameObjectWithTag("UIController");
		ui.GetComponent<UIController>().UIEnabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.transform.LookAt(player);
	}
}
