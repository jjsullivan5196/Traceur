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
        //For the capsule...
        transform.rotation = new Quaternion(0, 0, 0, 0);

        //Control
        //Left and right
        if (Input.GetButton("SwitchRight"))
        {
            slide.x = slideSpeed;
        }
        if (Input.GetButton("SwitchLeft"))
        {
            slide.x = -slideSpeed;
        }
        if (Input.GetButtonUp("SwitchLeft"))
        {
            slide.x = 0;
        }

        if (Input.GetButtonUp("SwitchRight"))
        {
            slide.x = 0;
        }

        //Jump and gravity
        if (!controller.isGrounded && jmp < 0)
        {
            slide.y = -4;
        }
        else
        {
            if (Input.GetButton("Jump"))
            {
                slide.y = 4;
                jmp = 0.4f;
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
