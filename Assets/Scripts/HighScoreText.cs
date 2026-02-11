using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HighScoreText : MonoBehaviour
{
    public Text LabelHighScore;

    private void OnEnable()
    {
        if (WinTrigger.Win)
        {
            LabelHighScore.text = ("Highscore time:" + System.Math.Round(WinTrigger.HighScore, 2)  + 
            "\n" + "Your current time:" + System.Math.Round(WinTrigger.CurrentTime, 2) +
            "\n" + "Distance walked: " + System.Math.Round(PlayerMove.Distance, 2));
        }
		else 
        {
            LabelHighScore.text = ("Try again!");
        }
    }
}