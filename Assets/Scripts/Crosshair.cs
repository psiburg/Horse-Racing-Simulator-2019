using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour {

	public RawImage crosshair;
	public Transform crosshairPoint;
	public Camera cam;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		Vector3 temp = cam.WorldToScreenPoint(crosshairPoint.position);
		crosshair.transform.position = new Vector3(temp.x, temp.y, 0f);
	}

}
