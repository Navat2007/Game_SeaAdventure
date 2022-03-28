using Managers;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private TMP_Text _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<TMP_Text>();
        CurrencyManager.instance.OnScoreChange += (s, score) => UpdateText(score);
    }

    void UpdateText(float value)
    {
        _scoreText.text = $"Score: {value}";
    }
}
