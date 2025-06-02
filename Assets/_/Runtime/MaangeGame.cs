using System;
using TMPro;
using UnityEngine;

public class MaangeGame : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private float _spawnTimer;
    public int score;
    private int maxHighScores = 5;
    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _spawnTimer)
        {
                Instantiate(_enemyPrefab);
                _timer = 0;
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
