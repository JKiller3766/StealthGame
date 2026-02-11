using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    public static float HighScore = float.MaxValue;
    public static float CurrentTime;
    public static bool Win = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Win = true;
        CurrentTime = Timer.TimePast;
        if (CurrentTime < HighScore)
        {
            HighScore = CurrentTime;
        }
        SceneManager.LoadScene("Ending");
    }
}
