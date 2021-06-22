using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarShooterScript : MonoBehaviour {

    public GameObject movingStar;

    private Vector3 spawnPosition;

	void Start () {

        Invoke("SpawnStar", 0); // Invokes the SpawnStar method on startup
    }
	
	void Update () {
        movingStar.transform.Translate(-2f * Time.deltaTime, 0, 0); // Moves the star gameobject at a certain speed

        spawnPosition = new Vector3(transform.position.x, transform.position.y, Random.Range(transform.position.z - 5, transform.position.z + 5)); // Sets Vector3 positions to location of the object the script is in, with z creating a random within 10 blocks range
    }

    void SpawnStar()
    {
        float delay = Random.Range(0.5f, 2f); // Creates random delay for each star spawn
        GameObject instantiatedStar = (GameObject)Instantiate(movingStar, spawnPosition, Quaternion.identity); // Instantiates a new gameobject based off the movingStar prefab, in spawnPosition, with default rotation aka none
        Destroy(instantiatedStar, 5); // Destroys instantited projectiles after 5 seconds

        Invoke("SpawnStar", delay); // Runs same method causing it to loop but on random delay
    }

}
