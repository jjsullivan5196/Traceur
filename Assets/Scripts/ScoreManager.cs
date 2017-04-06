/*
 Name: Brian Little
 Class: CST 306
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text text;
    private bool allowPoints;
	private float pntsCooldown;
    private float startPosition;
    private float score;
	private int multiplier = 1;

	// Use this for initialization
	void Start () {
		text = GameObject.FindGameObjectWithTag ("ScoreLabel").GetComponent<Text>();
        allowPoints = true;
        startPosition = transform.position.z;
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(pntsCooldown > 0)
        {
            pntsCooldown -= Time.deltaTime;
            //Debug.Log(pntsCooldown);

        }
        else
        {
            allowPoints = true;
			GameObject.FindGameObjectWithTag ("Flicker").GetComponent<Image> ().enabled = false;
        }

		if(allowPoints)
        {
            calculateScore();
        }
        text.text = "Score: " + score.ToString();
    }

    private void OnTriggerEnter(Collider col)
    {
		if(col.gameObject.tag == "Enemy"){
	        allowPoints = false;
			pntsCooldown = 5.0f;
			GameObject.FindGameObjectWithTag ("Flicker").GetComponent<Image> ().enabled = true;
			StartCoroutine ("blink");
	        //Debug.Log(cooldown);
		}
		if (col.gameObject.tag == "Multiplier") {
			StartCoroutine ("adjustScore");
			Destroy (col);
		}
    }

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
		float cd = 5.0f;
		float blinkInterval = .5f;
		while (cd > 0) {
			Debug.Log (blinkInterval);
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

	private IEnumerator adjustScore()
	{
		float cd = 5.0f;
		while (cd > 0) {
			cd -= Time.deltaTime;
			multiplier = 2;
			yield return null;
		}
		multiplier = 1;
	}
}
