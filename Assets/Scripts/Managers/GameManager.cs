using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    
    public EventHandler OnRestart;

    private GameState currentState = GameState.PAUSE;
    private int _score;
    private int _highScore;
    
    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        
        instance = this;
    }

    public void FinishLevel()
    {
        if (_score > _highScore)
            _highScore = _score;
        
        UiManager.instance.ShowLevelResult();
    }

}

public enum GameState
{
    PLAY,
    PAUSE
}
