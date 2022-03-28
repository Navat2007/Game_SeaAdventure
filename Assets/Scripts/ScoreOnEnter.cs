using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class ScoreOnEnter : MonoBehaviour
{
    [SerializeField] private float scoreToAdd = 10;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        CurrencyManager.instance.SetScore(scoreToAdd);
    }
}
