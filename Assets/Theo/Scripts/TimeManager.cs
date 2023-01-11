using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    GameObject pausMenu;
    GameObject settingsMenuIG;

    public bool paused;
    void Start()
    {
        pausMenu = GameObject.Find("PausMenu");
        settingsMenuIG = GameObject.Find("SettingsMenuIG");
    }

    // Update is called once per frame
    void Update()
    {
        if(paused)
        {
            Time.timeScale = 0;     
            Debug.Log("time scale 0");
        }
        else
        {
            Time.timeScale = 1;
            Debug.Log("time scale 1");
        }
    }

    public void PauseGame()
    {
        paused = true;
    }
    public void unPauseGame()
    {
        paused = false;
    }
}
