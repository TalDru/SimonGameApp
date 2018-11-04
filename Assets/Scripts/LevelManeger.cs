using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManeger : MonoBehaviour {

    public int Num;
    private MenuManeger theMM;
    public Button Easy;

    void Start()
    {
        Easy.Select();
        theMM = FindObjectOfType<MenuManeger>();
    }

    public void onClick()
    {
        theMM.Lvl = Num;
    }
}
