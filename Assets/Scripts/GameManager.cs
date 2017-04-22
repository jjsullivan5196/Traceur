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
    public float initTimer;
    private float timer;

	//Use this for initialization
	void Start () {
        difficulty = 1;
        lives = 3;
        timer = initTimer;
	}//end function
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Time: " + timer + " Difficulty: " + difficulty);
        timer -= Time.deltaTime;
        if(timer <= 0 && difficulty != 3)
        {
            difficulty++;
            timer = initTimer;
            Debug.Log(difficulty);
        }
        if(lives <= 0)
        {
            //End the game(Implement later)
            Debug.Log("Game Over");
        }
	}//end function

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            timer = initTimer;
            if(difficulty != 1)
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

    public int getDif()
    {
        return difficulty;
    }

    public int getLives()
    {
        return lives;
    }
}//end class
