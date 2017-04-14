﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoInput {
	public enum Move { Left, Right, Up, Down };
    public delegate void InputEvent(Move motion);
    public static event InputEvent MotionEvent;

	public static bool isRunning = false;

    public static void EvStepRight() { MotionEvent(Move.Right); }
    public static void EvStepLeft() { MotionEvent(Move.Left); }
    public static void EvJump() { MotionEvent(Move.Up); }
    public static void EvDuck() { MotionEvent(Move.Down); }
}

/*public class MyDemo
{
    void EventDo()
    {
        Debug.Log("I did something");
        MoInput.EvStepRight();
    }
    
    public MyDemo()
    {
        MoInput.StepLeft += EventDo;
    }
}
*/
