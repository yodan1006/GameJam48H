using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class MaangeGame : MonoBehaviour
{
    [SerializeField] private Défilement defilement;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private float _spawnTimer = 100f;
    [FormerlySerializedAs("_addSpeedModifier")] [SerializeField] private float _SpeedSpawn = 0.5f;
    [SerializeField] private float _timeToUpDifficulty = 10f;
    [SerializeField] private float _speedMove = 0.2f;
    [SerializeField] private float _speedMoveInitial;
    private float timer;

    public int score;

    private const int MaxHighScores = 5;

    private void Start()
    {
        defilement.timeToMove = _speedMoveInitial;
        StartCoroutine(Spawnenemy());
    }

    private void Update()
    {
        _textMesh.text = score.ToString();
        timer += Time.deltaTime;
        if (timer >= _timeToUpDifficulty)
        {
            timer = 0;
            _spawnTimer -= _SpeedSpawn;
            defilement.timeToMove = Mathf.Max(0.05f, defilement.timeToMove - _speedMove);
        }
    }

    private IEnumerator Spawnenemy()
    {
        while (true)
        {
            GameObject enemy =Instantiate(_enemyPrefab);
            Défilement defilScript = enemy.GetComponent<Défilement>();
            
            defilScript.Init(game: this);
            yield return new WaitForSeconds(_spawnTimer);
        }
    }
    public void AddScore(int points)
    {
        score += points;
    }

    public void CheckAndAddHighScore(int playerScore)
    {
        int[] highScores = LoadHighScores();

        for (int i = 0; i < MaxHighScores; i++)
        {
            if (playerScore > highScores[i])
            {
                for (int j = MaxHighScores - 1; j > i; j--)
                {
                    highScores[j] = highScores[j - 1];
                }

                highScores[i] = playerScore;
                SaveHighScores(highScores);
                return;
            }
        }
    }

    private int[] LoadHighScores()
    {
        int[] highScores = new int[MaxHighScores];
        for (int i = 0; i < MaxHighScores; i++)
        {
            highScores[i] = PlayerPrefs.GetInt("HighScore" + i, 0);
        }
        return highScores;
    }

    private void SaveHighScores(int[] highScores)
    {
        for (int i = 0; i < MaxHighScores; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, highScores[i]);
        }
        PlayerPrefs.Save();
    }
}
