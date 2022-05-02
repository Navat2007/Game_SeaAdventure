using System;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        public event Action OnExit;
    
        [SerializeField] private bool needRefillChunks = true;

        private void Awake()
        {
            GameManager.LevelManager = this;
        }

        public bool GetNeedRefillChunks => needRefillChunks;

        public void SetRefillChunks(bool value)
        {
            needRefillChunks = value;
        }
    
        public void StartLevel()
        {
            GameManager.SetState(GameState.PLAY);
        }
        
        public void PauseLevel()
        {
            GameManager.SetState(GameState.PAUSE);
        }
    
        public void ExitLevel()
        {
            OnExit?.Invoke();
            
            CurrencyManager.Instance.SetCurrency(Currency.SCORE, 0);
            CurrencyManager.Instance.SetCurrency(Currency.LEVEL_GOLD, 0);
            CurrencyManager.Instance.SetCurrency(Currency.AIR, 100);
            
            UiManager.Instance.ShowMainScreen();
        }

        public void FinishLevel()
        {
            PauseLevel();
            UiManager.Instance.ShowLevelResult();
            
            CurrencyManager.Instance.AddCurrency(Currency.GOLD, CurrencyManager.Instance.GetCurrency(Currency.LEVEL_GOLD));
        }
    }
}
