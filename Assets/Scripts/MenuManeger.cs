using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManeger : MonoBehaviour {

    public int Lvl;
    public Text HighScore;

	// Use this for initialization
	void Start () {
        if (!PlayerPrefs.HasKey("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", 0);
        } 
        HighScore.text = "High Score - " + PlayerPrefs.GetInt("HighScore");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
        if (!PlayerPrefs.HasKey("Lvl"))
        {
            PlayerPrefs.SetInt("Lvl", 1);
        }
        PlayerPrefs.SetInt("Lvl", Lvl);
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
