using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadGame : MonoBehaviour
{
    [SerializeField] private Défilement value;
    [SerializeField] private float valueBaseSpeed;
    public void Reload()
    {
            value.timeToMove = valueBaseSpeed;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

    public void StartGame()
    {
        value.timeToMove = valueBaseSpeed;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
