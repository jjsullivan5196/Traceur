using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /*
        AUTHORS: Truong Anthony - truo3110
        File Info: Unity Script made for player control, for Traceur Project 
    */

    private string currentTrack = "Middle";

    private float jmp = -5;
    private float trk = 0;
    private float laneScale = 5;
    private float cooldown = 5;
    private float timer;
    private float speed = 4;

    private void Start()
    {
        timer = cooldown;
    }

    void Update()
    {
        //For the capsule...
        transform.rotation = new Quaternion(0, 0, 0, 0);

        //Move !
        transform.GetComponent<Rigidbody>().velocity = new Vector3(trk, jmp, speed);

        //Jump !
        if (Input.GetButtonDown("Jump") && transform.position.y < 1.1f && jmp == -5)
        {
            jmp = laneScale;
            StartCoroutine(jump());
        }

        //Switching tracks
        if (Input.GetButtonDown("SwitchLeft") && currentTrack != "Left" && trk == 0)
        {
            if (currentTrack == "Right")
            {
                trk = laneScale * -1;
                StartCoroutine(switchTrk());
                currentTrack = "Middle";
            }
            else
            {
                trk = laneScale * -1;
                StartCoroutine(switchTrk());
                currentTrack = "Left";
            }
        }
        if (Input.GetButtonDown("SwitchRight") && currentTrack != "Right" && trk == 0)
        {
            if (currentTrack == "Left")
            {
                trk = laneScale;
                StartCoroutine(switchTrk());
                currentTrack = "Middle";
            }
            else
            {
                trk = laneScale;
                StartCoroutine(switchTrk());
                currentTrack = "Right";
            }
        }

        //Make it faster over time
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            speed *= 1.1f;
            timer = cooldown;
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
