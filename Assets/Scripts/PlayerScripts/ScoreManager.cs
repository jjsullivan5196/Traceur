/*
name: Brian Little
course: CST306
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

	private static bool invuln;
	private static float score;
	private static bool gameOver;

	public AudioClip impact;
	private AudioSource source;
    private bool encouragement;
    private bool allowPoints;
	private float pntsCooldown;
    private float invCooldown = 0;
    private float startPosition;
	private int multiplier = 1;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
        allowPoints = true;
        encouragement = false;
        invuln = false;
        startPosition = transform.position.z;
        score = 0;
	}

	// Update is called once per frame
	void Update () {
        int difficulty = GameManager.getDif();

        if(difficulty == 2){
            multiplier = 2;
        }
        else if(difficulty == 3){
            multiplier = 3;
        }

        if ((transform.position.z < 700 || (GameManager.getLives() > 0 && GameManager.mode == 1)) && !gameOver){
            if(invCooldown > 0){
                //Makes player undetecable(invulnerable)
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
                encouragement = true;
                invCooldown -= Time.deltaTime;
                //Debug.Log("Invulnerable");
            }
            else{
                //Makes player detecable(vulnerable)
                gameObject.GetComponent<CapsuleCollider>().enabled = true;
                encouragement = false;
            }

            if (pntsCooldown > 0){
                pntsCooldown -= Time.deltaTime;
                //Debug.Log(pntsCooldown);
            }
            else {
                allowPoints = true;
                invuln = false;
            }

            if (allowPoints){
                calculateScore();
            }
        }

        else
        {
			gameOver = true;
            if (Input.GetMouseButtonDown(0))
            {
                gameOver = false;
                GameManager.setLives(3);
                SceneManager.LoadScene("menu2");
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
		if(col.gameObject.tag == "Enemy")
        {
            Debug.Log("HIT");
            if(!invuln)
            {
				source.Play(0);
                allowPoints = false;
				invuln = true;
                pntsCooldown = 5.0f;
				if (GameManager.getLives () != 0 && GameManager.mode == 1) {
					GameManager.setLives (GameManager.getLives () - 1);
				}
				if(GameManager.getDif() != 1 && GameManager.dynamicDif) {
					GameManager.setDif (GameManager.getDif() - 1);
				}
            }
		}

		if (col.gameObject.tag == "Multiplier")
        {
			StartCoroutine ("adjustScore");
			Destroy (col.gameObject);
		}

        if(col.gameObject.tag == "DodgeCheck")
        {
            invCooldown = 1.0f;
        }
    }

    //Calculates score
    private void calculateScore()
    {
        float distance = transform.position.z - startPosition;
        if (distance >= 1) {
            startPosition = Mathf.Floor(transform.position.z);
            score += 10 * multiplier;
        }
    }
		
    //If player hits a multiplier object score is adjusted
	private IEnumerator adjustScore()
	{
		float cd = 5.0f;
		while (cd > 0) {
			cd -= Time.deltaTime;
            Debug.Log("Double Score");
			multiplier *= 2;
			yield return null;
		}
		multiplier = 1;
	}

	public static bool getInvuln(){
		return invuln;
	}

	public static float getScore(){
		return score;
	}

	public static bool isGameOver(){
		return gameOver;
	}

	public static void setGameOver(bool gmOv){
		gameOver = gmOv;
	}
}
