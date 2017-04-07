using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed;
    private bool left = true;
    private bool right;
    private bool leftToMid;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,transform.position.y,transform.forward.z+(1+transform.position.z)), 2f*Time.deltaTime);
        //Debug.Log(transform.forward);
    }
}
