/*
name: John Sullivan
couse: CST306
*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MotionControl : MonoBehaviour
{
    enum State
    {
        begin,
        promptRight,
        collectRight,
        calcRight,
        promptJump,
        collectJump,
        calcJump,
        end
    }

    public TextMesh uiText;
    State calibrateState;
    float actionTime;
    List<float> values;
    AccController mControl;
    string action;

	// Use this for initialization
	void Start ()
    {
        calibrateState = State.begin;
        actionTime = -0.1f;
        values = new List<float>();
        mControl = GetComponent<AccController>();
        MoInput.MotionEvent += HandleMotion;
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
                case State.begin:
                    {
                        mControl.enabled = false;
                        uiText.text = "Tap touchpad to begin calibration";
                        if (Input.GetMouseButtonDown(0))
                        {
                            actionTime = 3.0f;
                            calibrateState = State.promptRight;
                        }
                        break;
                    }
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
                        //SceneManager.LoadScene("test_level");
                        //enabled = false;
                        calibrateState = State.end;
                        break;
                    }
                case State.end:
                    {
                        mControl.enabled = true;
                        uiText.text = "Try moving left and right/jump to test the calibration\nPress BACK to try again\nTap the touchpad to continue";
                        uiText.text += "\nYour current action is: " + action;
                        if (Input.GetMouseButton(0))
                        {
                            SceneManager.LoadScene(MoInput.lastScene);
                        }
                        if (Input.GetMouseButton(1))
                        {
                            calibrateState = State.begin;
                        }
                        break;
                    }
            }
        }
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
        }
    }

}
