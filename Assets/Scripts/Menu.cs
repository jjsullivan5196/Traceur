/*

name: Anthony Truong

course: CST306

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject OptionMenu;
    public GameObject PlayMenu;

    public Text Difficulty;

    public void Start()
    {
        MainMenu.SetActive(true);
        OptionMenu.SetActive(false);
        PlayMenu.SetActive(false);
        Difficulty.text = "";
    }

    public void StartLive()
    {
        //Put the scene in the build settings, and just put the name of the scene 
        //in parameter of this function
        Debug.Log("Live");
        SceneManager.LoadScene("trackGeneration");
    }

    public void StartDistance()
    {
        //Put the scene in the build settings, and just put the name of the scene 
        //in parameter of this function
        Debug.Log("Distance");
        SceneManager.LoadScene("trackGeneration");
    }

    public void Calibrate()
    {
        //Put the calibrate scene here
        SceneManager.LoadScene("");
    }

    public void ActivateSettings()
    {
        MainMenu.SetActive(false);
        OptionMenu.SetActive(true);
    }

    public void ActivatePlay()
    {
        MainMenu.SetActive(false);
        PlayMenu.SetActive(true);
    }

    public void BackPlay()
    {
        MainMenu.SetActive(true);
        PlayMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Close game.");
        Application.Quit();
    }

    public void Back()
    {
        MainMenu.SetActive(true);
        OptionMenu.SetActive(false);
    }

    public void setOne()
    {
        //Put the line of code to set the difficulty
        Debug.Log("Difficulty = 1");
        Difficulty.text = ": 1";
    }

    public void setTwo()
    {
        //Put the line of code to set the difficulty
        Debug.Log("Difficulty = 2");
        Difficulty.text = ": 2";
    }

    public void setThree()
    {
        //Put the line of code to set the difficulty
        Debug.Log("Difficulty = 3");
        Difficulty.text = ": 3";
    }
}
