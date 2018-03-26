using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {
	
	void Start(){

	}
	
	void OnGUI () {

        GUI.Label(new Rect(10, 100, 250, 25), "CONTROLS:");
        GUI.Label(new Rect(10, 125, 250, 25), "W - Boost");
        GUI.Label(new Rect(10, 150, 250, 25), "S - Air Break");
        GUI.Label(new Rect(10, 175, 250, 25), "A - Rotate left");
        GUI.Label(new Rect(10, 200, 250, 25), "D - Rotate right");
        GUI.Label(new Rect(10, 225, 250, 25), "Q - Strafe Left");
        GUI.Label(new Rect(10, 250, 250, 25), "E - Strafe Left");
        GUI.Label(new Rect(10, 275, 250, 25), "Left Mouse - Fire Laser");
        GUI.Label(new Rect(10, 300, 250, 25), "Mouse look to rotate camera");

    }
	
}
