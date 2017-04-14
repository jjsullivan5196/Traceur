/*

name: Anthony Truong

course: CST306

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    private float timer;

	void Start ()
    {
        timer = 10;
	}
	
	void Update ()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(transform.gameObject);
        }
    }
}