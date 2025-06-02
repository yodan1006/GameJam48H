using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Défilement : MonoBehaviour
{
    public enum etat
    {
        Civil,
        Enemy
    };
    public float _speed;
    [SerializeField] private int addScore;
    etat _etat;
    private MaangeGame _game;
    private SpriteRenderer _spriteRenderer;
    private string _layer;
    [SerializeField] SpriteRenderer civil;
    [SerializeField] private SpriteRenderer enemy;


    private void Awake()
    {
        _game = FindObjectOfType<MaangeGame>();
        int random = Random.Range(0, 2);
        _etat = (etat)random;
    }

    private void Start()
    {
        if (_etat == etat.Enemy)
        {
            int layerIndex = LayerMask.NameToLayer("enemy");
            gameObject.layer = layerIndex;
            _layer = LayerMask.LayerToName(layerIndex);
        }
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
        DefineSprite();
    }

    public void DefineSprite()
    {
        switch (_etat)
        {
           //case etat.Civil: _spriteRenderer = civil; break;
           //case etat.Enemy: _spriteRenderer = enemy; break;
              case etat.Civil: _spriteRenderer.color = Color.blue; break;
              case etat.Enemy: _spriteRenderer.color = Color.cyan; break;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        if (_etat == etat.Civil)
        {
            _game.CheckAndAddHighScore(_game.score);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (_etat == etat.Enemy && other.CompareTag("bullet"))
        {
            AddScore(addScore);
        }
    }

    private void AddScore(int points)
    {
        _game.score += points;
    }
}
