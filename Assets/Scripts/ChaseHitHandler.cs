using UnityEngine;
using UnityEngine.SceneManagement;

public class ChaseHitHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<CarController>(out var carController))
        {
            GameStatus.PlayerWin = false;
            GameStatus.GameEnded = true;
            
            SceneManager.LoadScene(1);
        }
    }
}
