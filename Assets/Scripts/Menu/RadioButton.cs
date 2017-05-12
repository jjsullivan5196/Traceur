/*
name:John Sullivan
course: CST306
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRUI;

public class RadioButton : MonoBehaviour {

    public enum ButtonType
    {
        DIFFICULTY,
        SCALE,
        MODE,
        ACTION
    }

    public RadioSelect group;
    public RadioChoice OnSelect;
    public ButtonType Button;
    public int id = 0;
    private TextMesh mColor;
    public bool initial = false;
    private Vector3 init;
    private Vector3 zoom;
    private bool isZoomed;

	// Use this for initialization
	void Start () {
        mColor = GetComponent<TextMesh>();
        //mColor.color = initial ? Color.green : Color.white;
        init = transform.position;
        zoom = transform.position + -transform.forward * 2;
        isZoomed = false;

        switch (Button)
        {
            case ButtonType.DIFFICULTY:
                {
                    OnSelect = ChangeDifficulty;
                    mColor.color = GameManager.getDif() == id ? Color.green : Color.white;
                    break;
                }
            case ButtonType.MODE:
                {
                    OnSelect = ChangeMode;
                    mColor.color = GameManager.mode == id ? Color.green : Color.white;
                    break;
                }
            case ButtonType.SCALE:
                {
                    OnSelect = ChangeScaling;
                    mColor.color = GameManager.dynamicDif == (id == 0 ? false : true) ? Color.green : Color.white;
                    break;
                }
            case ButtonType.ACTION:
                {
                    OnSelect = Action;
                    break;
                }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (isZoomed)
        {
            transform.position = Vector3.Lerp(transform.position, zoom, 0.6f * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, init, 0.6f * Time.deltaTime);
        }
    }

    public void Click()
    {
        group(id);
    }

    public void Enter()
    {
        isZoomed = true;
    }

    public void Leave()
    {
        isZoomed = false;
    }

    public void onRadioSelect(int id)
    {
        if (this.id == id)
        {
            mColor.color = Color.green;
            OnSelect();
        }
        else
        {
            mColor.color = Color.white;
        }
    }

    public void ChangeDifficulty()
    {
        GameManager.setDif(id);
    }

    public void ChangeScaling()
    {
        GameManager.dynamicDif = id > 1 ? true : false;
    }

    public void ChangeMode()
    {
        GameManager.mode = id;
    }

    public void Action()
    {
        switch (id)
        {
            case 0:
                SceneManager.LoadScene("test_level");
                break;
            case 1:
                MoInput.lastScene = "menu2";
                SceneManager.LoadScene("ncalibrate");
                break;
        }
    }
}
