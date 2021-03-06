﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
public class AsteroidManager : MonoBehaviour{

    [SerializeField] GameObject[] asteroid;
    [SerializeField] int asteroidsOnAxisX;
	[SerializeField] int asteroidsOnAxisY;
	[SerializeField] int asteroidsOnAxisZ;
	[SerializeField] int gridSpacing = 400;

    void Start() {
        PlaceAsteroids();
    }

    void PlaceAsteroids() {
        for(int x = 0; x < asteroidsOnAxisX; x++){
			Debug.Log("x loop");
            for (int y = 0; y < asteroidsOnAxisY; y++){
				Debug.Log("y loop");
				for (int z = 0; z < asteroidsOnAxisZ; z++){
					Debug.Log("z loop ");
					InstantiateAsteroid(x, y, z);
                }
            }
        }
    }

    void InstantiateAsteroid(int x, int y, int z) {
		Debug.Log("spawning an asteroid");
		
		//instantiate random asteroid index at parent position + grid spacing + offset
		//is a child of the parent
        GameObject ast = Instantiate(asteroid[Random.Range(0,5)],
			transform.position + new Vector3(x * gridSpacing + AsteroidOffset(), 
											 y * gridSpacing + AsteroidOffset(), 
											 z * gridSpacing + AsteroidOffset()), 
			Quaternion.identity, 
			this.transform);
		Debug.Log("spawned an asteroid " + ast);
    }

    float AsteroidOffset() {
		return Random.Range(-gridSpacing / 2f, gridSpacing / 2f);
    }
}