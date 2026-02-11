using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextDistanceUI : MonoBehaviour
{
    public Text Label;

    private void FixedUpdate()
    {
        Label.text = ("Distance: " + System.Math.Round(PlayerMove.Distance, 2) + " m");
    }
}