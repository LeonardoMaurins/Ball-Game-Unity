﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondScript : MonoBehaviour {

	void Start () {
		
	}

	void Update () {
        transform.Rotate(0, 0, 50f * Time.deltaTime); 

	}
}
