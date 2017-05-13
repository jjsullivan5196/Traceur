/*
 name: Brian Little
 course: CST306
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour {

	private Vector3 target;
	private float speed;
	private bool moveL;
	// Use this for initialization
	void Start () {
		//target = gameObject.position;
		moveL = true;

	}
	
	// Update is called once per frame
	void Update () {
		target = gameObject.transform.position;
		if (GameManager.getDif () == 1) {
			speed = Random.Range (0.5f, 1.0f);
		} 
		else if (GameManager.getDif () == 2) {
			speed = Random.Range (1.0f, 1.5f);
		} 
		else if (GameManager.getDif () == 3) {
			speed = Random.Range (1.5f, 2.0f);
		}

		if (moveL) {
			target.x = -6;
			if (gameObject.transform.position.x < -2.9f) {
				moveL = false;
			}
		} else {
			target.x = 6;
			if (gameObject.transform.position.x > 2.9f) {
				moveL = true;
			}
		}

		transform.position = Vector3.Lerp(gameObject.transform.position, target, speed*Time.deltaTime);
	}
}
