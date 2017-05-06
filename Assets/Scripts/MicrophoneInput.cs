/*
name: John Sullivan
course: CST306
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneInput : MonoBehaviour
{
    AudioSource microphone;

	// Use this for initialization
	void Start ()
    {
        microphone = GetComponent<AudioSource>();
        microphone.clip = Microphone.Start(Microphone.devices[0], true, 10, 44100);
        microphone.loop = true;

        while (!(Microphone.GetPosition(null) > 0))
        {
            continue;
        }

        microphone.Play();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
