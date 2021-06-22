using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zAxisMovement : MonoBehaviour {

    public int zChange = 5;

    void Start () {
		
	}
	
	void FixedUpdate () {

        float zDirection = 1;

        if (transform.position.z > 10)
        {
            zChange = zChange * -1;
        }
        if (transform.position.z < -10)
        {
            zChange = zChange * -1;
        }

        transform.Translate(0, 0, zDirection * zChange * Time.fixedDeltaTime);

    }
}
