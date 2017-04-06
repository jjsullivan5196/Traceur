using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /*

    name: Anthony Truong

    course: CST306

    */

    private string currentTrack = "Middle";

    private float jmp = -5;
    private float trk = 0;
    private float laneScale = 5;
    private float cooldown = 5;
    private float timer;
    private float speed = 4;
    private float timerMiddle = 0;
    private float cooldownMiddle = 3;

    private void Start()
    {
        timer = cooldown;
        MoInput.StepRight += switchRight;
        MoInput.StepLeft += switchLeft;
        MoInput.Jump += jumpInput;
    }

    void Update()
    {
        //For the capsule...
        transform.rotation = new Quaternion(0, 0, 0, 0);

        //Move !
        transform.GetComponent<Rigidbody>().velocity = new Vector3(trk, jmp, speed);

        //Jump !
        if (Input.GetButtonDown("Jump"))
        {
            //jumpInput();
            MoInput.EvJump();
        }

        //Switching tracks
        if (Input.GetButtonDown("SwitchLeft"))
        {
            //switchLeft();
            MoInput.EvStepLeft();
        }
        if (Input.GetButtonDown("SwitchRight"))
        {
            //switchRight();
            MoInput.EvStepRight();
        }

        //Make it faster over time
        timer -= Time.deltaTime;
        timerMiddle -= Time.deltaTime;
        if (timerMiddle < 0)
        {
            if (currentTrack == "Left")
            {
                if (trk == 0)
                {
                    trk = laneScale;
                    StartCoroutine(switchTrk());
                    currentTrack = "Middle";
                }
            }
            else if (currentTrack == "Right")
            {
                if (trk == 0)
                {
                    trk = laneScale * -1;
                    StartCoroutine(switchTrk());
                    currentTrack = "Middle";
                }
            }
        }
        if (timer < 0)
        {
            speed *= 1.1f;
            timer = cooldown;
        }
    }

    private void switchLeft()
    {
        if (currentTrack != "Left")
        {
            if (currentTrack != "Right")
            {
                if (trk == 0)
                {
                    trk = laneScale * -1;
                    StartCoroutine(switchTrk());
                    currentTrack = "Left";
                    timerMiddle = cooldownMiddle;
                }
            }
        }
    }

    private void switchRight()
    {
        if (currentTrack != "Right")
        {
            if (currentTrack != "Left")
            {
                if (trk == 0)
                {
                    trk = laneScale;
                    StartCoroutine(switchTrk());
                    currentTrack = "Right";
                    timerMiddle = cooldownMiddle;
                }
            }
        }
    }

    private void jumpInput()
    {
        if (transform.position.y < 1.1f)
        {
            if (jmp == -5)
            {
                jmp = laneScale;
                StartCoroutine(jump());
            }
        }
    }

    IEnumerator switchTrk()
    {
        yield return new WaitForSeconds(.5f);
        trk = 0;
    }

    IEnumerator jump()
    {
        yield return new WaitForSeconds(.7f);
        jmp = laneScale * -1;
    }
}
