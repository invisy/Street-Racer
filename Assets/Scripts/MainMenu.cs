using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
