using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Défilement : MonoBehaviour
{
    public enum Etat
    {
        Civil,
        Enemy
    }
    private Transform _target;
    private float _speed;
    private Etat _etat;
    private MaangeGame _game;
    private SpriteRenderer _spriteRenderer;

    private bool _pretABouger = false;
    private bool _enPause = false;
    private bool _retour = false;

    public Vector2 dir = Vector2.left;
    public float distance = 1f;
    public float timeToMove = 1f;

    [SerializeField] private float timeToPause = 1f;
    [SerializeField] private int addScore = 10;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Deplacement());
    }

    public void Init(Transform target, MaangeGame game)
    {
        _target = target;
        _game = game;

        // Choisir aléatoirement le type
        _etat = (Etat)Random.Range(0, 2);

        if (_etat == Etat.Enemy)
        {
            gameObject.layer = LayerMask.NameToLayer("enemy");
        }
        DefineSprite();
    }
    private void OnBecameInvisible()
    {
        if (_retour)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Deplacement()
    {
        while (true)
        {
            transform.position += (Vector3)dir.normalized * distance;
            yield return new WaitForSeconds(timeToMove);
        }
    }

    private void DefineSprite()
    {
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        switch (_etat)
        {
            case Etat.Civil:
                _spriteRenderer.color = Color.blue;
                break;
            case Etat.Enemy:
                _spriteRenderer.color = Color.red;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_etat == Etat.Civil)
        {
            _game.CheckAndAddHighScore(_game.score);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (_etat == Etat.Enemy && other.CompareTag("bullet"))
        {
            _game.AddScore(addScore);
        }

        Destroy(gameObject);
    }
}
