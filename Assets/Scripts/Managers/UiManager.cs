using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager instance;
        
        [Header("Main")]
        [SerializeField] private Transform uiPanel;
        [SerializeField] private Transform gamePanel;
        [SerializeField] private Button startButton;
        
        [Header("Game")]
        [SerializeField] private Transform _gamePanel;
        [SerializeField] private Transform _resultPanel;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text goldText;
        [SerializeField] private TMP_Text levelGoldText;
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private TMP_Text airText;
        [SerializeField] private Button _restartButton;
        
        [SerializeField] private Timer timer;
 
        void Awake () 
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

        public async Task Init()
        {
            if(startButton != null)
                startButton.onClick.AddListener(() =>
                {
                    CurrencyManager.instance.SetCurrency(Currency.SCORE, 0);
                    CurrencyManager.instance.SetCurrency(Currency.LEVEL_GOLD, 0);
                    uiPanel.gameObject.SetActive(false);
                    gamePanel.gameObject.SetActive(true);
                    GameManager.instance.StartLevel();
                });
            
            if(_restartButton != null)
                _restartButton.onClick.AddListener(() =>
                {
                    CurrencyManager.instance.SetCurrency(Currency.SCORE, 0);
                    CurrencyManager.instance.SetCurrency(Currency.LEVEL_GOLD, 0);
                    SetPanel(Panels.GAME);
                    GameManager.instance.RestartLevel();
                });
        }
        
        public async Task Subscribe()
        {
            CurrencyManager.instance.OnGoldChange += (sender, d) => UpdateCurrency(sender, d, Currency.GOLD);
            CurrencyManager.instance.OnLevelGoldChange += (sender, d) => UpdateCurrency(sender, d, Currency.LEVEL_GOLD);
            CurrencyManager.instance.OnAirChange += (sender, d) => UpdateCurrency(sender, d, Currency.AIR);
            CurrencyManager.instance.OnScoreChange += (sender, d) => UpdateCurrency(sender, d, Currency.SCORE);

            if (timer != null && timeText != null)
            {
                timer.OnTimerChange += (sender, time) =>
                {
                    TimeSpan timeSpan = TimeSpan.FromSeconds(time);
                    timeText.text = $"Time: {timeSpan:m\\:ss}";
                };
            }
        }
        
        public void SetPanel(Panels panel)
        {
            _gamePanel.gameObject.SetActive(false);
            _resultPanel.gameObject.SetActive(false);

            switch (panel)
            {
                case Panels.GAME:
                    _gamePanel.gameObject.SetActive(true);
                    break;
                case Panels.RESULT:
                    _resultPanel.gameObject.SetActive(true);
                    break;
            }
        }
        
        void UpdateCurrency(object obj, int value, Currency currency)
        {
            switch (currency)
            {
                case Currency.GOLD:
                    if (goldText != null)
                        goldText.text = $"Gold: {value}";
                    break;
                case Currency.LEVEL_GOLD:
                    if (levelGoldText != null)
                        levelGoldText.text = $"Gold: {value}";
                    break;
                case Currency.AIR:
                    if (airText != null)
                        airText.text = $"Air: {value}";
                    break;
                case Currency.SCORE:
                    if (scoreText != null)
                        scoreText.text = $"Score: {value}";
                    break;
            }
        }

        public void ShowGame()
        {
            SetPanel(Panels.GAME);
        }
        
        public void ShowLevelResult()
        {
            //scoreText.SetText($"Your score: {CurrencyManager.instance.GetCurrency(Currency.SCORE)}");
        
            SetPanel(Panels.RESULT);
        }
    }
    
    public enum Panels
    {
        GAME,
        RESULT
    }
}