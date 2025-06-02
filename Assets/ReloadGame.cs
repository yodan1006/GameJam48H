using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ReloadGame : MonoBehaviour
{
    [SerializeField] private Défilement value;
    [SerializeField] private float valueBaseSpeed;
    public void Reload(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            value.timeToMove = valueBaseSpeed;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
        }
    }
}
