using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JNIAssist;
using AccStuff;

public class AccController : MonoBehaviour {

	Text uiText;
	LinearAcceleration linacc;
	public float LRThreshold = 3.0f;
	public float UpDownThreshold = 20.0f;
	public float ActionDecay = 1.0f;
	public bool Debug = false;
	float actionTime = 0.0f;
	float runTime = 0.0f;
	string action = "";

	// Use this for initialization
	void Start () {
		JNMan.Init();
		linacc = new LinearAcceleration(JNMan.Context);

		if(Debug)
			uiText = GameObject.Find("Text").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 acc = linacc.accelerationVec();

		if (acc.y >= 4 && runTime <= 0.0f)
		{
			MoInput.isRunning = true;
			runTime = ActionDecay;
		}
		else if(runTime <= 0.0f)
		{
			MoInput.isRunning = false;
		}

		if (acc.x >= LRThreshold && actionTime <= 0.0f)
		{
			MoInput.EvStepRight();
			action = "RIGHT";
			actionTime = ActionDecay;
		}
		else if (acc.x <= -LRThreshold && actionTime <= 0.0f)
		{
			MoInput.EvStepLeft();
			action = "LEFT";
			actionTime = ActionDecay;
		}
		else if (acc.y >= UpDownThreshold && actionTime <= 0.0f)
		{
			MoInput.EvJump();
			action = "JUMP";
			actionTime = ActionDecay;
		}
		else if (acc.y <= -UpDownThreshold && actionTime <= 0.0f)
		{
			MoInput.EvDuck();
			action = "DUCK";
			actionTime = ActionDecay;
		}
		
		if (actionTime > 0.0f)
		{
			actionTime -= Time.deltaTime;
		}
		if (runTime > 0.0f)
		{
			runTime -= Time.deltaTime;
		}

		if(Debug)
		{
			uiText.text = acc.ToString();
			uiText.text += "\n" + action;
			uiText.text += MoInput.isRunning ? "\nRUNNING" : "";
		}
	}
}
