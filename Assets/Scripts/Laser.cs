using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(LineRenderer))]
[DisallowMultipleComponent]

public class Laser : MonoBehaviour{
    [SerializeField] float laserOffTime = .1f;
    [SerializeField] float maxDistance = 300f;
    [SerializeField] float fireDelay = 2f;
    LineRenderer lr;
    Light laserLight;
	public int damage = 20;

    bool canFire;

    void Awake(){
        lr = GetComponent<LineRenderer>();
        laserLight = GetComponent<Light>();
    }

    void Start(){
        lr.enabled = false;
        laserLight.enabled = false;
        canFire = true;
    }

    Vector3 CastRay() {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward) * maxDistance;
        if (Physics.Raycast(transform.position, fwd, out hit)){
            Debug.Log("Hit " + hit.transform);

			Destructible temp = hit.transform.GetComponent<Destructible>();

			//deal damage if enemy
			if (temp != null) {
				hit.transform.GetComponent<Destructible>().TakeDamage(damage);
				temp.SpawnExplosion(hit.point);
			}                
            return hit.point;
        }
        Debug.Log("Miss");
        return transform.position + transform.forward * maxDistance;
    }

    public void FireLaser(Vector3 targetPosition){
        if (canFire){
			lr.SetPosition(0, transform.position);
			lr.SetPosition(1, CastRay());
			lr.enabled = true;
			laserLight.enabled = true;
			canFire = false;
			if (GetComponent<AudioSource>() != null)
				GetComponent<AudioSource>().Play(); //play sound effect
			Invoke("TurnOffLaser", laserOffTime);
			Invoke("CanFire", fireDelay);
		}
	}

    void TurnOffLaser(){
        lr.enabled = false;
        laserLight.enabled = false;
    }

    public float Distance{
        get{
            return maxDistance;
        }
    }

    void CanFire(){
        canFire = true;
    }
}
