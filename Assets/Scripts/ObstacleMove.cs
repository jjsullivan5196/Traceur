/*
 name: Brian Little
 course: CST306
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour {

	private Vector3 target;
	private bool moveL;
	// Use this for initialization
	void Start () {
		//target = gameObject.position;
		moveL = true;
	}
	
	// Update is called once per frame
	void Update () {
		target = gameObject.transform.position;

		if (moveL) {
			target.x = -3;
			if (gameObject.transform.position.x < -2.9f) {
				moveL = false;
			}
		} else {
			target.x = 3;
			if (gameObject.transform.position.x > 2.9f) {
				moveL = true;
			}
		}

		transform.position = Vector3.Lerp(gameObject.transform.position, target, .05f);
	}
}
