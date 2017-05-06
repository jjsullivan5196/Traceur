/*
name: Brian Little
course: CST306
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static int lives;
	public float initTimer;
	public bool dynamicDif;

    //1 for Easy; 2 for Medium; 3 for Hard.
    private static int difficulty;
    private float timer;

	void Start () {
        difficulty = 1;
        lives = 3;
        timer = initTimer;
	}

	void Update () {
		
        timer -= Time.deltaTime;
        if(timer <= 0 && difficulty != 3 && dynamicDif)
        {
            difficulty++;
            timer = initTimer;
            Debug.Log(difficulty);
        }
        if(lives <= 0)
        {
			ScoreManager.setGameOver(true);
        }
	}//end function

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy") {
			if (ScoreManager.getInvuln() == false) {
				timer = initTimer;
				if (difficulty != 1) {
					difficulty--;
				}
				lives--;
			}
        }

        if(other.tag == "Life") {
            if(lives < 3){
                lives++;
            }
        }

    }//end function

    public static int getDif()
    {
        return difficulty;
    }

	public static void setDif(int dif){
		difficulty = dif;
	}

    public static int getLives()
    {
        return lives;
    }
}//end class
