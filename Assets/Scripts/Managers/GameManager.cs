using System;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public event Action OnExit;
        public event Action<GameState> OnGameStateChange;

        [SerializeField] private GameState currentState = GameState.PAUSE;

        public GameState GetState => currentState;
    
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
        
        public void StartLevel()
        {
            SetState(GameState.PLAY);
        }
        
        public void PauseLevel()
        {
            SetState(GameState.PAUSE);
        }

        public void ExitLevel()
        {
            PauseLevel();
            OnExit?.Invoke();
        }

        public void FinishLevel()
        {
            PauseLevel();
            UiManager.Instance.ShowLevelResult();
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