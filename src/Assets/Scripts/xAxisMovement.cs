using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xAxisMovement : MonoBehaviour {

    public int xChange = 5;

    void Start(){

    }

    void FixedUpdate() {

        float xDirection = 1;

        if (transform.position.x > 10)
        {
            xChange = xChange * -1;
        }
        if (transform.position.x < -10)
        {
            xChange = xChange * -1;
        }

        transform.Translate(xDirection * xChange * Time.fixedDeltaTime, 0, 0);

    }
}
