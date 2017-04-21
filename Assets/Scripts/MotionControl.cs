/*
name: John Sullivan
couse: CST306
*/

using System.Collections;
using System.Collections.Generic;
using JNIAssist;
using AccStuff;
using UnityEngine;
using UnityEngine.UI;

public class MotionControl : MonoBehaviour
{
    LinearAcceleration acc;
    public Text uiText;

	// Use this for initialization
	void Start ()
    {
        acc = new LinearAcceleration();
	}
	
	// Update is called once per frame
	void Update ()
    {
        uiText.text = ((Vector3)acc).ToString();
    }
}
