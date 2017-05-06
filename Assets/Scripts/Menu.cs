/*

name: Anthony Truong

course: CST306

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public Canvas PlayMenu;
    public Canvas OptionMenu;

    public Button Play;
    public Button Option;
    public Button Quit;
    public Button Live;
    public Button Distance;

    private void Start()
    {
        PlayMenu = PlayMenu.GetComponent<Canvas>();
        Play = Play.GetComponent<Button>();
        Quit = Quit.GetComponent<Button>();
        Live = Live.GetComponent<Button>();
        Distance = Distance.GetComponent<Button>();
        PlayMenu.enabled = false;
        OptionMenu.enabled = false;
    }

    public void clickOnPlay()
    {
        PlayMenu.enabled = true;
        Play.enabled = false;
        Option.enabled = false;
        Quit.enabled = false;
    }

    public void clickOnOption()
    {
        OptionMenu.enabled = true;
        Play.enabled = false;
        Option.enabled = false;
        Quit.enabled = false;
    }

    public void clickOnBackPlay()
    {
        PlayMenu.enabled = false;
        Play.enabled = true;
        Option.enabled = true;
        Quit.enabled = true;
    }

    public void clickOnBackOption()
    {
        Debug.Log("BackOption");
        OptionMenu.enabled = false;
        Play.enabled = true;
        Option.enabled = true;
        Quit.enabled = true;
    }

    public void clickOnExit()
    {
        Application.Quit();
    }

    public void clickOnLive()
    {
        //LaunchSceneLive
        Debug.Log("Launch Scene Live");
    }

    public void clickOnDistance()
    {
        //LaunchSceneDistance
        Debug.Log("Launch Scene Distance");
    }
}
