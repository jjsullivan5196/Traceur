/*
name:John Sullivan
course: CST306
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRUI;

public class RadioGroup : MonoBehaviour {

    public RadioSelect evt;
    public RadioButton[] choices;

	// Use this for initialization
	void Start () {
		foreach(var choice in choices) 
        {
            evt += choice.onRadioSelect;
        }
        foreach (var choice in choices)
        {
            choice.group = evt;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
