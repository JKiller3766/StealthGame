using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class endingText : MonoBehaviour
{
    public Text labelTitle;


    private void OnEnable()
    {
        if (WinTrigger.win)
        {
            labelTitle.text = ("Congratulations!");
        } else
        {
            labelTitle.text = ("Game over!");
        }
        
    }
}