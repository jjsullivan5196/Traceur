/*
name: Brian Little
course: CST306
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static int lives;
	public float initTimer;
	public static bool dynamicDif = false;
    public static int mode = 0;
    public static bool pause = false;
    public GameObject pauseMenu;

    //1 for Easy; 2 for Medium; 3 for Hard.
    private static int difficulty = 1;
    private float timer;

	void Start () {
        //difficulty = 1;
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
        if(lives <= 0 && mode == 1)
        {
			ScoreManager.setGameOver(true);
        }
        if (Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene("menu2");
        }
	}//end function

    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "Enemy") {
			timer = initTimer;
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

	public static void setLives(int L){
		lives = L;
	}
		
}//end class
