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
    public float RunDecay = 3.0f;
	public bool Debug = false;
	float actionTime = 0.0f;
	float runTime = 0.0f;
	string action = "";
	Vector3 acc;
    
	void Start () {
		JNMan.Init();
		linacc = new LinearAcceleration(JNMan.Context);

		if(Debug) {
			uiText = GameObject.Find("Text").GetComponent<Text>();
			MoInput.MotionEvent += HandleMotion;
		}
			
	}
	
	void Update () {
		acc = linacc.accelerationVec();

		if (acc.y >= 4 && runTime <= 0.0f)
		{
			MoInput.isRunning = true;
			runTime = RunDecay;
		}
		else if(runTime <= 0.0f)
		{
			MoInput.isRunning = false;
		}

		if (acc.x >= LRThreshold && actionTime <= 0.0f)
		{
			MoInput.EvStepLeft();
			actionTime = ActionDecay;
		}
		else if (acc.x <= -LRThreshold && actionTime <= 0.0f)
		{
			MoInput.EvStepRight();
			actionTime = ActionDecay;
		}

		if (acc.y >= UpDownThreshold && actionTime <= 0.0f)
		{
			MoInput.EvJump();
			actionTime = ActionDecay;
		}
		else if (acc.y <= -UpDownThreshold && actionTime <= 0.0f)
		{
			MoInput.EvDuck();
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
			DebugUpdate();
	}

	void HandleMotion(MoInput.Move motion)
	{
		switch(motion)
		{
			case MoInput.Move.Left:
				action = "LEFT";
				break;
			case MoInput.Move.Right:
				action = "RIGHT";
				break;
			case MoInput.Move.Up:
				action = "JUMP";
				break;
			case MoInput.Move.Down:
				action = "DUCK";
				break;
		}
	}

	void DebugUpdate()
	{
		uiText.text = acc.ToString();
		uiText.text += "\n" + action;
		uiText.text += MoInput.isRunning ? "\nRUNNING" : "";
	}
}
