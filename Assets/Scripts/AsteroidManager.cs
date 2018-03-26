using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]

public class AsteroidManager : MonoBehaviour{

    [SerializeField] Asteroid[] asteroid;
    [SerializeField] int asteroidsOnAxis = 2;
    [SerializeField] int gridSpacing = 100;

    void Start(){
        PlaceAsteroids();
    }

    void PlaceAsteroids(){
        for(int x = 0; x < asteroidsOnAxis; x++){
            for (int y = 0; y < asteroidsOnAxis; y++){
                for (int z = 0; x < asteroidsOnAxis; z++){
                    InstantiateAsteroid(x, y, z);
                }
            }
        }
    }

    void InstantiateAsteroid(int x, int y, int z){
        Instantiate(asteroid[Random.Range(0,4)], 
			new Vector3(transform.position.x + (x * gridSpacing) + AsteroidOffset(), 
				transform.position.y + (y * gridSpacing) + AsteroidOffset(), 
				transform.position.z + (z * gridSpacing) + AsteroidOffset()), 
			Quaternion.identity);
    }

    float AsteroidOffset(){
		return Random.Range(-gridSpacing / 2f, gridSpacing / 2f);
    }
}