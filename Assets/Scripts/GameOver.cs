using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;
    private void Awake()
    {
        _textMesh = GameObject.FindWithTag("GameOverText").GetComponent<TextMeshProUGUI>();

        if (GameStatus.PlayerWin)
            _textMesh.SetText("You win!");
        else
            _textMesh.SetText("You lose!");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(2);
    }
}
