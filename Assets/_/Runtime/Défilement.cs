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
    public Etat _etat;
    private MaangeGame _game;
    private GameObject _visuel;
    private bool _retour = false;

    public Vector2 dir = Vector2.left;
    public float distance = 1f;
    public float timeToMove = 1f;

    [SerializeField] private int addScoreToKill = 100;
    [SerializeField] private GameObject _prefabCivil;
    [SerializeField] private GameObject _prefabEnemy;

    private void Start()
    {
        StartCoroutine(Deplacement());
    }

    public void Init(MaangeGame game)
    {
        _game = game;

        // Choisir aléatoirement le type
        _etat = (Etat)Random.Range(0, 2);

        if (_etat == Etat.Enemy)
        {
            gameObject.layer = LayerMask.NameToLayer("enemy");
        }
        DefineVisuel();
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

    private void DefineVisuel()
    {
        if (_visuel != null) Destroy(_visuel);

        GameObject prefabToUse = null;
        switch (_etat)
        {
            case Etat.Civil:
                prefabToUse = _prefabCivil;
                break;
            case Etat.Enemy:
                prefabToUse = _prefabEnemy;
                break;
        }

        if (prefabToUse != null)
        {
            _visuel = Instantiate(prefabToUse, transform);
            _visuel.transform.localPosition = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_etat == Etat.Civil)
        {
            _game.CheckAndAddHighScore(_game.score);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (_etat == Etat.Enemy)
        {
            _game.AddScore(addScoreToKill);
        }
        Destroy(gameObject);
    }
}
