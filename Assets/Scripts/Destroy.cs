﻿/*

name: Anthony Truong

course: CST306

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    GameObject player;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update ()
    {
        if (player.transform.position.z - 15 > transform.position.z)
        {
            Destroy(transform.gameObject);
        } 
    }
}