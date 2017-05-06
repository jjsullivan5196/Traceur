/*
name: John Sullivan
couse: CST306
*/

using UnityEngine;

public class Track : MonoBehaviour
{
	float init_x;
	float init_y;
	float cur_x;
	float cur_y;
	public float UpDown = 6.0f;
	public float LeftRight = 2.0f;
	public float Forward = 2.0f;
	public float TimeCooldown = 1.0f;
	public bool Debug = false;
	Vector3 target;
	float cooldown;

	// Use this for initialization
	void Start ()
    {
		init_x = transform.position.x;
		init_y = transform.position.y;
		cur_x = init_x;
		cur_y = init_y;

		MoInput.MotionEvent += HandleMotion;
	}

    // Update is called once per frame
    void Update()
    {
		if(!Debug)
		{
        	if (transform.position.z < 700 && MoInput.isRunning) 
			{ 
				target = transform.position + transform.forward * Forward;
			}
		}
		else
		{
			if (transform.position.z < 700)
			{ 
				target = transform.position + transform.forward * Forward;
			}
		}
        target.x = cur_x;
        target.y = cur_y;

        if(Input.GetButtonDown("SwitchLeft"))
        {
            MoInput.EvStepLeft();
		}
		if(Input.GetButtonDown("SwitchRight"))
		{
			MoInput.EvStepRight();
		}
		else if(Input.GetButtonDown("Jump"))
		{
			MoInput.EvJump();
		}
		else if(Input.GetButtonDown("Duck"))
		{
			MoInput.EvDuck();
		}

        if (cooldown > 0.0f)
        {
            cooldown -= Time.deltaTime;
        }
        else if (cooldown <= 0.0f)
        {
            cur_x = init_x;
            cur_y = init_y;
        }

		transform.position = Vector3.Lerp(transform.position, target, 4.0f * Time.deltaTime);
	}

	void HandleMotion(MoInput.Move m)
	{
		if(cooldown <= 0.0f)
		{
			switch(m)
			{
				case MoInput.Move.Left:
                    {
                        cur_x = -LeftRight;
                        cur_y = init_y;
                        break;
                    }
				case MoInput.Move.Right:
                    {
                        cur_x = LeftRight;
                        cur_y = init_y;
                        break;
                    }
				case MoInput.Move.Up:
                    {
                        cur_y = UpDown;
                        cur_x = init_x;
                        break;
                    }
				case MoInput.Move.Down:
                    {
                        cur_y = -UpDown / 4;
                        cur_x = init_x;
                        break;
                    }
			}

			cooldown = TimeCooldown;
		}
	}
}
