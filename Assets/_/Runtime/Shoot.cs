using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Shoot : MonoBehaviour
{ 
    private float _speedShoot = 100;
    [SerializeField] private GameObject _prefabShoot;
    private GameObject bulette;

    public void Shooter(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 direction = Vector2.right;
            KillThen(direction);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void KillThen(Vector2 direction)
    {
        bulette = Instantiate(_prefabShoot);
        bulette.transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
        Rigidbody2D rb = bulette.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction.normalized * _speedShoot;
    }
}
