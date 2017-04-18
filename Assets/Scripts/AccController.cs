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

	Text uiText;
	LinearAcceleration linacc;

	float LRThreshold;
	float UpDownThreshold;

	public float ActionDecay = 1.0f;
    public float RunDecay = 3.0f;
	public bool Debug = false;

	float actionTime = 0.0f;
	float runTime = 0.0f;
	string action = "";

	Vector3 acc;

    bool calibrate = true;
    bool calibrate_step = true;
    List<float> lrvalues = new List<float>();
    List<float> udvalues = new List<float>();
    float calibrate_timer = 3.0f;
    float step_timer = 1.0f;

	// Use this for initialization
	void Start ()
    {
		JNMan.Init();
		linacc = new LinearAcceleration(JNMan.Context);

		if(Debug) {
			uiText = GameObject.Find("Text").GetComponent<Text>();
			MoInput.MotionEvent += HandleMotion;
		}
			
	}
	
	// Update is called once per frame
	void Update ()
    {
        acc = linacc.accelerationVec();

        if (calibrate)
        {
            if(calibrate_step)
            {
                if (calibrate_timer >= 0.0f)
                {
                    uiText.text = "Step right in " + calibrate_timer;
                    calibrate_timer -= Time.deltaTime;
                    return;
                }
                else if(step_timer >= 0.0f)
                {
                    uiText.text = "Step right NOW";
                    lrvalues.Add(acc.x);
                    step_timer -= Time.deltaTime;
                    return;
                }
                LRThreshold = Mathf.Abs(lrvalues.Max()) > Mathf.Abs(lrvalues.Min()) ?
                    lrvalues.Max() :
                    lrvalues.Min();
                calibrate_step = false;
                calibrate_timer = 3.0f;
                step_timer = 1.0f;
                return;
            }
            else
            {
                if (calibrate_timer >= 0.0f)
                {
                    uiText.text = "Jump in " + calibrate_timer;
                    calibrate_timer -= Time.deltaTime;
                    return;
                }
                else if (step_timer >= 0.0f)
                {
                    uiText.text = "Jump NOW";
                    udvalues.Add(acc.y);
                    step_timer -= Time.deltaTime;
                    return;
                }
                UpDownThreshold = udvalues.Max();
            }
            calibrate = false;
        }

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
        uiText.text = string.Format("LR: {0} UD: {1}\n", LRThreshold, UpDownThreshold);
		uiText.text += acc.ToString();
		uiText.text += "\n" + action;
		uiText.text += MoInput.isRunning ? "\nRUNNING" : "";
	}
}
