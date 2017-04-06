using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoInput {
    public delegate void InputEvent();
    public static event InputEvent StepRight;
    public static event InputEvent StepLeft;
    public static event InputEvent Jump;
    public static event InputEvent Duck;

	public static bool isRunning = false;

    public static void EvStepRight() { StepRight(); }
    public static void EvStepLeft() { StepLeft(); }
    public static void EvJump() { Jump(); }
    public static void EvDuck() { Duck(); }
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
