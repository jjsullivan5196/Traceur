using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text text;
    private bool allowPoints;
    private float cooldown;
    private float startPosition;
    private float score;

	// Use this for initialization
	void Start () {
        allowPoints = true;
        startPosition = transform.position.z;
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            Debug.Log(cooldown);
        }
        else
        {
            allowPoints = true;
        }

		if(allowPoints)
        {
            calculateScore();
        }
        text.text = "Score: " + score.ToString();
    }

    private void OnTriggerEnter(Collider col)
    {
        allowPoints = false;
        cooldown = 5.0f;
        //Debug.Log(cooldown);
    }

    private void calculateScore()
    {
        float distance = transform.position.z - startPosition;
        if (distance > 1) {
            startPosition = Mathf.Floor(transform.position.z);
            score += 10;
        }
    }
}
