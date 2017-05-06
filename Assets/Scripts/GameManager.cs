/*
name: Brian Little
course: CST306
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    
    public static int lives;
    //1 for Easy; 2 for Medium; 3 for Hard.
    private static int difficulty;
    public bool dynDif;
    public float initTimer;
    private float timer;

	//Use this for initialization
	void Start () {
        //Set default
        if(lives == 0)
        {
            lives = 3;
        }
        if (initTimer == 0)
        {
            initTimer = 15;
        }
        if(difficulty == 0)
        {
            difficulty = 1;
        }
        timer = initTimer;
	}//end function
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Time: " + timer + " Difficulty: " + difficulty);
        if (dynDif)
        {
            timer -= Time.deltaTime;
            if (timer <= 0 && difficulty != 3)
            {
                difficulty++;
                timer = initTimer;
                Debug.Log("Difficulty: " + difficulty);
            }
            if (lives <= 0)
            {
                //End the game(Implement later)
                Debug.Log("Game Over");
            }
        }
	}//end function

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            timer = initTimer;
            if(difficulty != 1 && dynDif)
            {
                difficulty--;
            }
            lives--;
        }

        if(other.tag == "Life")
        {
            if(lives < 3)
            {
                lives++;
            }
        }

    }//end function

    //Draw lives.. doesn't work yet
    void onGUI()
    {
        var texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
        texture.SetPixel(0, 0, new Color(1.0f, 1.0f, 1.0f));

        if (lives == 1)
        {
            GUI.DrawTexture(new Rect(800.0f, -75.0f, 100.0f, 200.0f), texture);
        }
        if(lives > 1)
        {
            //GUI.DrawTexture(new Rect(800.0f, -75.0f, 100.0f, 200.0f), texture);
        }
    }//end function

    public static int getDif()
    {
        return difficulty;
    }

    public int getLives()
    {
        return lives;
    }
    
    public void setDif(int dif)
    {
        difficulty = dif;
    }

    public void setDynDif(bool d)
    {
        dynDif = d;
    }
}//end class
