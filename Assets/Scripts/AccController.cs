/*
name: John Sullivan
couse: CST306
*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using JNIAssist;
using AccStuff;

public class AccController : MonoBehaviour
{
	public Text uiText;
	LinearAcceleration linacc;

	public float ActionDecay = 0.5f;
    public float RunDecay = 3.0f;
	public bool Debug = false;

	float actionTime = 0.0f;
	float runTime = 0.0f;
	string action = "";

	Vector3 acc;

	// Use this for initialization
	void Start ()
    {
		JNMan.Init();
		linacc = new LinearAcceleration();

		if(Debug) {
			MoInput.MotionEvent += HandleMotion;
		}
	}
	
	// Update is called once per frame
	void Update ()
    {
        acc = (Vector3)linacc;

        if (acc.y >= 4 && runTime <= 0.0f)
		{
			MoInput.isRunning = true;
			runTime = RunDecay;
		}
		else if(runTime <= 0.0f)
		{
			MoInput.isRunning = false;
		}

		if (acc.x >= MoInput.thresholdLR && actionTime <= 0.0f)
		{
			MoInput.EvStepLeft();
			actionTime = ActionDecay;
		}
		else if (acc.x <= -MoInput.thresholdLR && actionTime <= 0.0f)
		{
			MoInput.EvStepRight();
			actionTime = ActionDecay;
		}

		if (acc.y >= MoInput.thresholdUD && actionTime <= 0.0f)
		{
			MoInput.EvJump();
			actionTime = ActionDecay;
		}
		else if (acc.y <= -MoInput.thresholdUD && actionTime <= 0.0f)
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
        switch (motion)
        {
            case MoInput.Move.Left:
                {
                    action = "LEFT";
                    break;
                }
            case MoInput.Move.Right:
                {
                    action = "RIGHT";
                    break;
                }
            case MoInput.Move.Up:
                {
                    action = "JUMP";
                    break;
                }
            case MoInput.Move.Down:
                {
                    action = "DUCK";
                    break;
                }
		}
	}

	void DebugUpdate()
	{
        uiText.text = string.Format("LR: {0} UD: {1}\n", MoInput.thresholdLR, MoInput.thresholdUD);
		uiText.text += acc.ToString();
		uiText.text += "\n" + action;
		uiText.text += MoInput.isRunning ? "\nRUNNING" : "";
	}
}
