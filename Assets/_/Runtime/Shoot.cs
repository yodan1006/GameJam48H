using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Shoot : MonoBehaviour
{ 
    private float _speedShoot = 50;
    [SerializeField] private GameObject _prefabShoot;
    [SerializeField] private MaangeGame _game;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private int _pointToKillEnemy;
    [SerializeField] private GameObject _player;
    [SerializeField] private AudioSource _audioSlash;

    public void Shooter(InputAction.CallbackContext context)
    {
        // Seulement quand la touche est pressée (pas maintenue)
        if (context.performed)
        {
            _audioSlash.Play();
            KillThen();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) _game.AddScore(_pointToKillEnemy);
        _game.CheckAndAddHighScore(_game.score);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void KillThen()
    {
        _animator.SetBool("Attaque", true);
        _collider.enabled = true;
        
        // Désactiver le collider après un court délai
        Invoke(nameof(DisableCollider), 0.2f);
    }
    
    private void DisableCollider()
    {
        _animator.SetBool("Attaque", false);
        _collider.enabled = false;
    }
}