using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour {

	public RankingsArray[] placements;
	private int totalRacers = 8;
	private int racerFields = 3;

	private bool spaceLevel = false;
	// Use this for initialization
	void Start () {
		Scene level = SceneManager.GetActiveScene();
		if (level.buildIndex == 2)
			spaceLevel = true;

		if (spaceLevel)
			ZalgoText();
	}

	// Update is called once per frame
	void Update () {

	}

	void ZalgoText() {
		for (int i = 1; i < totalRacers; i++) {
			for (int j = 0; j < racerFields; j++) {
				placements[i].racer[j].textBox.text = "?????";
				Debug.Log(placements[i].racer[j].textBox.text.Length);
			}
		}

		//player specifically
		placements[0].racer[0].textBox.text = "1st";
		placements[0].racer[1].textBox.text = "God-King Horsevald";
		placements[0].racer[2].textBox.text = "∞";
	}
}

[System.Serializable]
public class RankingsArray {

	[System.Serializable]
	public struct Rankings {
		public Text textBox;
	}
	
	public Rankings[] racer = new Rankings[3];

}