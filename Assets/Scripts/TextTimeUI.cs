using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextTimeUI : MonoBehaviour
{
    public Text Label;

    private void FixedUpdate()
    {
        Label.text = ("Time: " + System.Math.Round(Timer.TimePast, 0) + " s");
    }
}