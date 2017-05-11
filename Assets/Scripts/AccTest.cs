/*
name: John Sullivan
couse: CST306
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JNIAssist;
using AccStuff;

public class AccTest : MonoBehaviour {

	Text uiText;
	LinearAcceleration linacc;
	float decay = 1.0f;
	float actionTime = 0.0f;
	string action = "";

	// Use this for initialization
	void Start ()
    {
		uiText = GameObject.Find("Text").GetComponent<Text>();
		linacc = new LinearAcceleration();
	}
	
	// Update is called once per frame
	void Update ()
    {
		Vector3 acc = linacc.accelerationVec();

		if (acc.x >= 6 && actionTime <= 0.0f)
		{
			action = "LEFT";
			actionTime = decay;
		}
		if (acc.x <= -6 && actionTime <= 0.0f)
		{
			action = "RIGHT";
			actionTime = decay;
		}
		if (acc.y >= 12 && actionTime <= 0.0f)
		{
			action = "JUMP";
			actionTime = decay;
		}
		if (actionTime > 0.0f)
		{
			actionTime -= Time.deltaTime;
		}

		uiText.text = acc.ToString();
		uiText.text += "\n" + action;
	}
}
