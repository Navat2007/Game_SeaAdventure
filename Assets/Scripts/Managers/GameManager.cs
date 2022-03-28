using System;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public EventHandler OnRestart;

        [SerializeField] private GameState currentState = GameState.PAUSE;
        private int _score;
        private int _highScore;
    
        private void Awake()
        {
            if(instance != null)
                Destroy(gameObject);
        
            instance = this;
        }

        private async void Start()
        {
            await SaveLoadManager.instance.Init();
            await SettingsManager.instance.Init();
            await CurrencyManager.instance.Init();
            await UiManager.instance.Init();
        }

        public void FinishLevel()
        {
            if (_score > _highScore)
                _highScore = _score;
        
            UiManager.instance.ShowLevelResult();
        }

    }
}

public enum GameState
{
    PLAY,
    PAUSE
}