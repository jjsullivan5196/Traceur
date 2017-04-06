using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingMovement : MonoBehaviour
{
    /*

    name: Anthony Truong

    course: CST306

    */

    private Vector3 slide;
    private Vector3 middle;

    private CharacterController controller;

    private float jmp = 0;
    private float slideSpeed = 4;
    private float speed = 4;
    private float timer = 0;
    private float cooldown = 5;

    void Start()
    {
        timer = cooldown;
        controller = transform.GetComponent<CharacterController>();
    }

    void Update()
    {
        middle = new Vector3(0, transform.position.y, transform.position.z);

        //For the capsule...
        transform.rotation = new Quaternion(0, 0, 0, 0);

        //Control
        //Left and right
        if (Input.GetButton("SwitchRight"))
        {
            slide.x = slideSpeed;
        }
        else
        {
            if (Input.GetButton("SwitchLeft"))
            {
                slide.x = -slideSpeed;
            }
            else
            {
                slide.x = 0;
            }
        }

        //Jump and gravity
        if (controller.isGrounded && Input.GetButton("Jump"))
        {
            slide.y = 4;
            jmp = 0.5f;
        }
        else
        {
            if (jmp < 0)
            {
                slide.y = -4;
            }
        }

        //Forward
        slide.z = speed;

        //Compute
        controller.Move(slide * Time.deltaTime);

        //Make it faster over time
        timer -= Time.deltaTime;
        jmp -= Time.deltaTime;
        if (timer < 0)
        {
            speed *= 1.1f;
            timer = cooldown;
        }
    }
}
