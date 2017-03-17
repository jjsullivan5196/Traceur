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
    private string orientation = "North";

    private float timer = 5;
    private float grounded = 0;

    private void Start()
    {
        transform.GetComponent<Rigidbody>().AddForce(transform.forward * 250f);
    }

    void Update()
    {
        //For the capsule...
        transform.rotation = new Quaternion(0, 0, 0, 0);

        //Jump !
        if (grounded > 0)
        {
            grounded -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && grounded <= 0)
        {
            transform.GetComponent<Rigidbody>().AddForce(transform.up * 350f);
            grounded = 2;
        }

        //Switching tracks
        if (Input.GetButtonDown("SwitchLeft") && currentTrack != "Left")
        {
            
            if (currentTrack == "Right")
                currentTrack = "Middle";
            else
                currentTrack = "Left";
        }
        if (Input.GetButtonDown("SwitchRight") && currentTrack != "Right")
        {
            Debug.Log("Go right !");
            if (currentTrack == "Left")
                currentTrack = "Middle";
            else
                currentTrack = "Right";
        }

        //Make it faster over time
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            //Debug.Log("up !");
            transform.GetComponent<Rigidbody>().AddForce(transform.forward * 1.1f);
            timer = 5;
        }
    }
}
