using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HighScoreText : MonoBehaviour
{
    public Text labelHighScore;


    private void OnEnable()
    {
        labelHighScore.text = ("Highscore time:" + System.Math.Round(WinTrigger.highScore, 2)  + 
            "\n" + "Your current time:" + System.Math.Round(WinTrigger.currentTime, 2) +
            "\n" + "Distance walked: " + System.Math.Round(PlayerMove.distance, 2));
    }
}