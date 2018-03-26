using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Laser[] laser;

    void Update(){
        if (Input.GetAxis("Fire1") > 0){
            foreach (Laser l in laser){
                Vector3 pos = transform.position + transform.forward * l.Distance;
                l.FireLaser(pos);
            }
        }
    }
}