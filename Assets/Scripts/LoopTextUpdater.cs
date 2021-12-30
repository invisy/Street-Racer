using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoopTextUpdater : MonoBehaviour
{
    public TextMeshProUGUI LoopTextUi;

    public FinishLineHitHandler FinishLineHitHandler;

    private void Start()
    {
        LoopTextUi.text = $"Loop: 1";
    }

    private void FixedUpdate()
    {
        LoopTextUi.text = $"Loop: {FinishLineHitHandler.LoopNumber}";
    }
}
