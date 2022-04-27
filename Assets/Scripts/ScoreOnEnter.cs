using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class ScoreOnEnter : MonoBehaviour
{
    [SerializeField] private int scoreToAdd = 10;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        CurrencyManager.Instance.AddCurrency(Currency.SCORE, scoreToAdd);
    }
}
