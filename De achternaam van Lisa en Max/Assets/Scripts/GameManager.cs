using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Color> colors;
    public Material materialBase;
    public Material materialBase2;
    public int timesBeaten = 0;

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        materialBase.color = colors[timesBeaten];
        materialBase2.color = colors[timesBeaten];
    }

    public void UpdateMaterial()
    {
        timesBeaten++;

        if (timesBeaten <= colors.Count)
        {
            materialBase.color = colors[timesBeaten];
            materialBase2.color = colors[timesBeaten];
        }
    }

    public void Update()
    {
        if (materialBase.color != colors[timesBeaten])
        {
            materialBase.color = colors[timesBeaten];
            materialBase2.color = colors[timesBeaten];
        }

        if (GameObject.Find("Player"))
        {
            GameObject.Find("Player").GetComponentInChildren<Text>().text = "Wins: " + timesBeaten;
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }
}
