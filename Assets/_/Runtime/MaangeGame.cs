using System;
using TMPro;
using UnityEngine;

public class MaangeGame : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _timeToSpawn;
    [SerializeField] private TextMeshProUGUI _textMesh;
    private float _timer;
    public int score;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _timeToSpawn)
        {
            Instantiate(_enemyPrefab);
            _timer = 0;
        }
        
        _textMesh.text = score.ToString();
    }
}
