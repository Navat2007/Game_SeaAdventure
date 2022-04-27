using System;
using Managers;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private TMP_Text _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        CurrencyManager.Instance.OnScoreChange += UpdateText;
    }

    private void OnDisable()
    {
        CurrencyManager.Instance.OnScoreChange -= UpdateText;

    }

    void UpdateText(object sender, int value)
    {
        if(_scoreText != null)
            _scoreText.text = $"Score: {value}";
    }
}
