/*
 name: Brian Little
 course: CST306
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DisplayText : MonoBehaviour {

	private TextMesh tScore;
	// Use this for initialization
	void Start () {
		tScore = GameObject.FindGameObjectWithTag ("ScoreLabel").GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.getDif () == 1) {
			tScore.text = "Difficulty: Easy";
		} else if (GameManager.getDif () == 2) {
			tScore.text = "Difficulty: Medium";
		} else if (GameManager.getDif () == 3) {
			tScore.text = "Difficulty: Hard";
		}
		tScore.text += "\nLives: " + GameManager.getLives() + "\nScore: " + ScoreManager.getScore();

		if(ScoreManager.isGameOver()){
			tScore.text = "Final Score: " + ScoreManager.getScore() + "\nTap touchpad to restart!";
		}
	}
}
