using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class Asteroid : MonoBehaviour{
    [SerializeField] float minScale = .8f;
    [SerializeField] float maxScale = 1.2f;
    [SerializeField] float rotationOffset = 100f;

    Transform myT;
    Vector3 randomRotation;

    void Awake(){
        myT = transform;
    }

    void Start(){
        //size
        Vector3 scale = Vector3.one;
        scale.x = Random.Range(minScale, maxScale) * myT.localScale.x;
        scale.y = Random.Range(minScale, maxScale) * myT.localScale.y;
        scale.z = Random.Range(minScale, maxScale) * myT.localScale.z;

        myT.localScale = scale;

        //rotation
        randomRotation.x = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.y = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.z = Random.Range(-rotationOffset, rotationOffset);
    }

    void Update(){
        myT.Rotate(randomRotation * Time.deltaTime);
    }
}
    