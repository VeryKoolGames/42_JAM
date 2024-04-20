using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _currentScore;

    [SerializeField] private OnEnemyDestroyedListener OnEnemyKilled;

    [SerializeField] private TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        _currentScore = 0;
        OnEnemyKilled.OnEnemyDestroyed += updateScore;
    }

    public void updateScore(int scoreToBeAdded)
    {
        _currentScore += scoreToBeAdded;
        scoreText.text = "SCORE : " + _currentScore;
    }

}
