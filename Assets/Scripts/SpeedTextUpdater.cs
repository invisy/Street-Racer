using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedTextUpdater : MonoBehaviour
{

    public TextMeshProUGUI speedTextUi;

    public CarPhysics CarPhysics;

    private void Start()
    {
        speedTextUi.text = $"Speed: 20 km/h";
    }

    private void FixedUpdate()
    {
        speedTextUi.text = $"Speed, km/h: {Math.Round((CarPhysics.Velocity.magnitude), 1)}";
    }
}
