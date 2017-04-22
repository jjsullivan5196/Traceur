/*
name: John Sullivan
couse: CST306
*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MotionControl : MonoBehaviour
{
    enum State
    {
        promptRight,
        collectRight,
        calcRight,
        promptJump,
        collectJump,
        calcJump
    }

    public Text uiText;
    State calibrateState;
    float actionTime;
    List<float> values;

	// Use this for initialization
	void Start ()
    {
        calibrateState = State.promptRight;
        actionTime = 3.0f;
        values = new List<float>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (actionTime > 0.0f)
        {
            actionTime -= Time.deltaTime;

            switch(calibrateState)
            {
                case State.promptRight:
                    {
                        uiText.text = "Step right in " + actionTime.ToString("0");
                        break;
                    }
                case State.collectRight:
                    {
                        uiText.text = "Step right NOW";
                        values.Add(((Vector3)MoInput.imu).x);
                        break;
                    }
                case State.calcRight:
                    {
                        MoInput.thresholdLR = values.Max();
                        values.Clear();
                        actionTime = 0.0f;
                        break;
                    }
                case State.promptJump:
                    {
                        uiText.text = "Jump in " + actionTime.ToString("0");
                        break;
                    }
                case State.collectJump:
                    {
                        uiText.text = "Jump NOW";
                        values.Add(((Vector3)MoInput.imu).y);
                        break;
                    }
                case State.calcJump:
                    {
                        MoInput.thresholdUD = values.Max();
                        values.Clear();
                        actionTime = 0.0f;
                        break;
                    }
            }
        }
        else
        {
            switch (calibrateState)
            {
                case State.promptRight:
                    {
                        calibrateState = State.collectRight;
                        actionTime = 1.0f;
                        break;
                    }
                case State.collectRight:
                    {
                        calibrateState = State.calcRight;
                        actionTime = 3.0f;
                        break;
                    }
                case State.calcRight:
                    {
                        calibrateState = State.promptJump;
                        actionTime = 3.0f;
                        break;
                    }
                case State.promptJump:
                    {
                        calibrateState = State.collectJump;
                        actionTime = 1.0f;
                        break;
                    }
                case State.collectJump:
                    {
                        calibrateState = State.calcJump;
                        actionTime = 3.0f;
                        break;
                    }
                case State.calcJump:
                    {
                        GetComponent<AccController>().enabled = true;
                        enabled = false;
                        break;
                    }
            }
        }
    }
}
