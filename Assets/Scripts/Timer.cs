using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private int scoreOnTime = 5;
    [SerializeField] private float currentTime;

    public EventHandler<float> OnTimerChange;

    private float _scoreTimerMax = 1f;
    private float _scoreTimer = 0;

    private void Update()
    {
        if (GameManager.instance.GetState == GameState.PLAY)
        {
            currentTime += Time.deltaTime;
            OnTimerChange?.Invoke(this, currentTime);

            _scoreTimer += Time.deltaTime;

            if (_scoreTimer >= _scoreTimerMax)
            {
                _scoreTimer = 0;
                CurrencyManager.instance.AddCurrency(Currency.SCORE, scoreOnTime);
            }
        }
    }
    
    public void Reset()
    {
        currentTime = 0;
    }
}
