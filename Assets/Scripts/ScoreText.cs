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
        CurrencyManager.instance.OnScoreChange += UpdateText;
    }

    private void OnDisable()
    {
        CurrencyManager.instance.OnScoreChange -= UpdateText;

    }

    void UpdateText(object sender, int value)
    {
        if(_scoreText != null)
            _scoreText.text = $"Score: {value}";
    }
}
