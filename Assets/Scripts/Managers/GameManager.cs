using System;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public static PlayerManager PlayerManager;
        public static LevelManager LevelManager;

        public static event Action<GameState> OnGameStateChange;

        private static GameState _currentState = GameState.MENU;

        public GameState GetState => _currentState;
    
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            } 
            else if (Instance != this)
            {
                Destroy (gameObject);
            }
        }

        private async void Start()
        {
            await SaveLoadManager.Instance.Init();
            await SettingsManager.Instance.Init();
            await CurrencyManager.Instance.Init();
            await UiManager.Instance.Init();

            await UiManager.Instance.Subscribe();
            await SettingsManager.Instance.Subscribe();
        }

        public static void SetState(GameState state)
        {
            _currentState = state;
            OnGameStateChange?.Invoke(_currentState);
        }
    }
}

public enum GameState
{
    MENU,
    PLAY,
    PAUSE
}