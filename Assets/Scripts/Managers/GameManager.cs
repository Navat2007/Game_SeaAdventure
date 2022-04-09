using System;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public event Action OnRestart;
        public event Action<GameState> OnGameStateChange;

        [SerializeField] private GameState currentState = GameState.PAUSE;

        public GameState GetState => currentState;
    
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            } 
            else if (instance != this)
            {
                Destroy (gameObject);
            }
        }

        private async void Start()
        {
            await SaveLoadManager.instance.Init();
            await SettingsManager.instance.Init();
            await CurrencyManager.instance.Init();
            await UiManager.instance.Init();
        }
        
        public void StartLevel()
        {
            SetState(GameState.PLAY);
        }
        
        public void PauseLevel()
        {
            SetState(GameState.PAUSE);
        }

        public void RestartLevel()
        {
            OnRestart?.Invoke();
            StartLevel();
        }

        public void FinishLevel()
        {
            PauseLevel();
            UiManager.instance.ShowLevelResult();
        }

        public void SetState(GameState state)
        {
            currentState = state;
            OnGameStateChange?.Invoke(currentState);
        }
    }
}

public enum GameState
{
    PLAY,
    PAUSE
}