/**
 * Controls player based on mouse movement. Static speed value 
 * and no rotation.
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]

public class PlayerControl : MonoBehaviour
{
	public int maxSpeed = 20;
	public int minSpeed = 5;
	public int defaultSpeed = 13;
	public int currentSpeed;
	public float rollSpeed = 100f;
	public float turnSpeed = 0.1f;
	public float strafeSpeed = 10f;

	private GameObject[] turbines;
	public float lowTurbines = 0.3f;
	public float defaultTurbines = 0.55f;
	public float highTurbines = 0.65f;

	void OnEnable(){
		turbines = GameObject.FindGameObjectsWithTag("Turbine");
		currentSpeed = defaultSpeed;
	}

	private void Update() {

	}

	void LateUpdate() {

		//A is roll left, D is roll right
		if (Input.GetKey(KeyCode.A)) 
			transform.Rotate(0, 0, Time.deltaTime * rollSpeed);
		else if (Input.GetKey(KeyCode.D))
			transform.Rotate(0, 0, -Time.deltaTime * rollSpeed);

		//Q is strafe left, E is strafe right
		if (Input.GetKey(KeyCode.Q))
			transform.Translate(-Time.deltaTime*strafeSpeed, 0, 0);
		else if (Input.GetKey(KeyCode.E))
			transform.Translate(Time.deltaTime*strafeSpeed, 0, 0);

		//Max speed
		if (Input.GetKey(KeyCode.W)){
			currentSpeed = maxSpeed;
			MaxTurbines(highTurbines);
		}//Min speed
		else if (Input.GetKey(KeyCode.S)){
			currentSpeed = minSpeed;
			MaxTurbines(lowTurbines);
		}//Cruise speed
		else{
			currentSpeed = defaultSpeed;
			MaxTurbines(defaultTurbines);
		}

		//move ship towards location based on cursor location
		{
			Vector3 mp = Input.mousePosition; //store mouse pos this frame
											  //enforce a dead zone in the middle of the screen for rotation
			//if (Mathf.Abs((mp.x - Screen.width/2)) > 2 && Mathf.Abs((mp.y - Screen.height/2)) > 2) {
			Vector3 mouseMovement = (mp - (new Vector3(Screen.width, Screen.height, 0) / 2.0f)) * turnSpeed;
			transform.Rotate(new Vector3(-mouseMovement.y, mouseMovement.x, -mouseMovement.x) * 0.025f);
			transform.Translate(Vector3.forward * Time.deltaTime*currentSpeed);
			//}

			//move forward no matter what
			MoveForward();
		}
	}

	public void MoveForward() {
		transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed);
	}
	public void MaxTurbines(float intensity){
		foreach (GameObject turbine in turbines)
        {
            turbine.GetComponent<LensFlare>().brightness = intensity;
        }
	}

}

