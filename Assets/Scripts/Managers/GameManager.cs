using System;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public EventHandler OnRestart;

        [SerializeField] private GameState currentState = GameState.PAUSE;

        public GameState GetState => currentState;
    
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
            UiManager.instance.ShowLevelResult();
        }

    }
}

public enum GameState
{
    PLAY,
    PAUSE
}