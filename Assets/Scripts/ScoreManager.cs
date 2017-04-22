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

    private Text tScore;
    private bool flicker;
    private bool encouragement;
    private bool allowPoints;
	private float pntsCooldown;
    private float invCooldown = 0;
    private float startPosition;
    private float score;
	private int multiplier = 1;

	// Use this for initialization
	void Start () {
        
        tScore = GameObject.FindGameObjectWithTag ("ScoreLabel").GetComponent<Text>();
        //encouragement = GameObject.FindGameObjectWithTag("Encouragement").GetComponent<Text>();
        allowPoints = true;
        encouragement = false;
        flicker = false;
        startPosition = transform.position.z;
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        GameManager GMscript = GameObject.Find("Player").GetComponent<GameManager>();
        int difficulty = GMscript.getDif();

        if(difficulty == 2)
        {
            multiplier = 2;
        }
        else if(difficulty == 3)
        {
            multiplier = 3;
        }

        if (transform.position.z < 514)
        {
 
            if(invCooldown > 0)
            {
                //Makes player undetecable(invulnerable)
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
                encouragement = true;
                invCooldown -= Time.deltaTime;
                //Debug.Log("Invulnerable");
            }
            else
            {
                //Makes player detecable(vulnerable)
                gameObject.GetComponent<CapsuleCollider>().enabled = true;
                encouragement = false;
            }

            if (pntsCooldown > 0)
            {
                pntsCooldown -= Time.deltaTime;
                //Debug.Log(pntsCooldown);
            }
            else
            {
                allowPoints = true;
                flicker = false;
                GameObject.FindGameObjectWithTag("Flicker").GetComponent<Image>().enabled = false;
            }

            if (allowPoints)
            {
                calculateScore();
            }

            if (!encouragement)
            {
                tScore.text = "Score: " + score.ToString();
            }
            else
            {
                tScore.text = "Score: " + score.ToString() + "\nGood Dodge!";
            }
        }

        else
        {
            tScore.text = "Score: " + score.ToString() + "\nTap touchpad to restart!";
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
		if(col.gameObject.tag == "Enemy")
        {
            if(!flicker)
            {
                allowPoints = false;
                pntsCooldown = 5.0f;
                GameObject.FindGameObjectWithTag("Flicker").GetComponent<Image>().enabled = true;
                StartCoroutine("blink");
                //Debug.Log("Begin Flickering");
            }
	        //Debug.Log(cooldown);
		}

		if (col.gameObject.tag == "Multiplier")
        {
			StartCoroutine ("adjustScore");
			Destroy (col.gameObject);
		}

        if(col.gameObject.tag == "DodgeCheck")
        {
            //Debug.Log("HIT");
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

	//Coroutine allows screen to blink once every .5 seconds
	private IEnumerator blink()
	{
        flicker = true;
		float cd = 5.0f;
		float blinkInterval = .5f;
		while (cd > 0) {
			//Debug.Log (blinkInterval);
			cd -= Time.deltaTime;
			blinkInterval -= Time.deltaTime;

			if (blinkInterval <= 0) {
				blinkInterval = .5f;
				if(GameObject.FindGameObjectWithTag ("Flicker").GetComponent<Image> ().enabled == true)
					GameObject.FindGameObjectWithTag ("Flicker").GetComponent<Image> ().enabled = false;
				else
					GameObject.FindGameObjectWithTag ("Flicker").GetComponent<Image> ().enabled = true;
			}
			yield return null;
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
}
