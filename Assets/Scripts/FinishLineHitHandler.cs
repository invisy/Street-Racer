using System;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FinishLineHitHandler : MonoBehaviour
{
    private const int Loops = 3;	
    public int LoopNumber = 1;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<CarController>(out var carController))
        {
           StartNextLoop();
        }
    }

    private void StartNextLoop()
    {
        LoopNumber++;

	CheckWinCondition();  	
    }

    private void CheckWinCondition()
    {
	if (LoopNumber >= Loops)
        {
            GameStatus.PlayerWin = true;
            GameStatus.GameEnded = true;
            SceneManager.LoadScene(2);
        }   
    }	
}
