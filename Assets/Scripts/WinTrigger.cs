using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    public static float highScore;
    public static float currentTime;
    public static bool win = false;
    
    public void Start()
    {
        highScore = float.MaxValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        win = true;
        currentTime = Timer.TimePast;
        if (currentTime < highScore)
        {
            highScore = currentTime;
        }
        SceneManager.LoadScene("Ending");
    }
}
