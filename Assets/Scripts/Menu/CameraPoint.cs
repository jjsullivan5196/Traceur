/*
name: John Sullivan
course: CST306
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPoint : MonoBehaviour {

    private GameObject focus;

	// Use this for initialization
	void Start ()
    {
        focus = null;
	}
	
	// Update is called once per frame
	void Update () {
        Ray mRay = new Ray(transform.position, transform.forward);
        RaycastHit mHit;

        if (Physics.Raycast(mRay, out mHit))
        {
            GameObject hit = mHit.collider.gameObject;
            if (hit.tag == "RadioButton")
            {
                if (focus == null)
                {
                    focus = hit;
                }
                if (focus != hit)
                {
                    focus.SendMessage("Leave");
                    focus = hit;
                    focus.SendMessage("Enter");
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && focus != null)
        {
            focus.SendMessage("Click");
        }
    }
}
