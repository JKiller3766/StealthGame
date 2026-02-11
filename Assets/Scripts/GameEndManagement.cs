using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class endingText : MonoBehaviour
{
    public Text LabelTitle;

    private void OnEnable()
    {
        if (WinTrigger.Win)
        {
            LabelTitle.text = ("Congratulations!");
        } else
        {
            LabelTitle.text = ("Game over!");
        }
    }
}