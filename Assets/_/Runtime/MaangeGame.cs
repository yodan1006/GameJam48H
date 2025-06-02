using System;
using TMPro;
using UnityEngine;

public class MaangeGame : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private float _spawnTimer;
    [SerializeField] private float _addSpeedModifier;
    [SerializeField] private float _TimeToUpDifficulty;
    [SerializeField] private Défilement _script;
    public int score;
    private int maxHighScores = 5;
    private float _timer;
    private GameObject _objet;
    private float _timerToDifficulty;

    private void Update()
    {
        _timer += Time.deltaTime;
        _timerToDifficulty += Time.deltaTime;
        if (_timer >= _spawnTimer)
        {
                _objet =Instantiate(_enemyPrefab);
                _timer = 0;
                if (_timerToDifficulty >= _TimeToUpDifficulty)
                {
                    _script._speed += _addSpeedModifier;
                    _timerToDifficulty = 0;
                }
        }
        _textMesh.text = score.ToString();
    }
    public void CheckAndAddHighScore(int playerScore)
    {
        int[] highScores = LoadHighScores();
        
        for (int i = 0; i < maxHighScores; i++)
        {
            if (playerScore > highScores[i])
            {
                for (int j = maxHighScores - 1; j > i; j--)
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
        int[] highScores = new int[maxHighScores];
        for (int i = 0; i < maxHighScores; i++)
        {
            highScores[i] = PlayerPrefs.GetInt("HighScore" + i, 0);
        }
        return highScores;
    }
    
    private void SaveHighScores(int[] highScores)
    {
        for (int i = 0; i < maxHighScores; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, highScores[i]);
        }
        PlayerPrefs.Save();
    }
}
