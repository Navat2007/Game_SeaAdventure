using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager Instance;
        
        [Header("Main")]
        [SerializeField] private Transform mainPanel;
        [SerializeField] private Transform gamePanel;
        
        [SerializeField] private Button menuStartButton;
        
        [Header("Game")]
        [SerializeField] private Transform levelGamePanel;
        [SerializeField] private Transform levelResultPanel;
        [SerializeField] private Transform levelPausePanel;
        [SerializeField] private Transform levelSettingPanel;
        
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text goldText;
        [SerializeField] private TMP_Text levelGoldText;
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private TMP_Text airText;
        
        [SerializeField] private Button levelPauseButton;
        [SerializeField] private Button levelPauseSettingButton;
        [SerializeField] private Button levelPauseSettingCloseButton;
        [SerializeField] private Button levelPauseResumeButton;
        [SerializeField] private Button levelPauseExitButton;
        
        [SerializeField] private Timer timer;
 
        void Awake () 
        {
            if (Instance is null)
            {
                Instance = this;
            } 
            else if (Instance != this)
            {
                Destroy (gameObject);
            }

            if (mainPanel is not null && gamePanel is not null)
            {
                mainPanel.gameObject.SetActive(true);
                gamePanel.gameObject.SetActive(false);
            }
           
        }

        public async Task Init()
        {
            if(menuStartButton is not null)
                menuStartButton.onClick.AddListener(() =>
                {
                    CurrencyManager.Instance.SetCurrency(Currency.SCORE, 0);
                    CurrencyManager.Instance.SetCurrency(Currency.LEVEL_GOLD, 0);
                    mainPanel.gameObject.SetActive(false);
                    gamePanel.gameObject.SetActive(true);
                    GameManager.Instance.StartLevel();
                });
            
            if(levelPauseButton is not null)
                levelPauseButton.onClick.AddListener(() =>
                {
                    GameManager.Instance.PauseLevel();
                    levelPausePanel.gameObject.SetActive(true);
                });
            
            if(levelPauseSettingButton is not null)
                levelPauseSettingButton.onClick.AddListener(() =>
                {
                    levelSettingPanel.gameObject.SetActive(true);
                });
            
            if(levelPauseSettingCloseButton is not null)
                levelPauseSettingCloseButton.onClick.AddListener(() =>
                {
                    levelSettingPanel.gameObject.SetActive(false);
                });
            
            if(levelPauseResumeButton is not null)
                levelPauseResumeButton.onClick.AddListener(() =>
                {
                    levelPausePanel.gameObject.SetActive(false);
                    GameManager.Instance.StartLevel();
                });
            
            if(levelPauseExitButton is not null)
                levelPauseExitButton.onClick.AddListener(() =>
                {
                    GameManager.Instance.ExitLevel();
                    
                    CurrencyManager.Instance.SetCurrency(Currency.SCORE, 0);
                    CurrencyManager.Instance.SetCurrency(Currency.LEVEL_GOLD, 0);
                    
                    levelPausePanel.gameObject.SetActive(false);
                    levelResultPanel.gameObject.SetActive(false);
                    gamePanel.gameObject.SetActive(false);
                    mainPanel.gameObject.SetActive(true);
                });
        }
        
        public async Task Subscribe()
        {
            CurrencyManager.Instance.OnGoldChange += (sender, d) => UpdateCurrency(sender, d, Currency.GOLD);
            CurrencyManager.Instance.OnLevelGoldChange += (sender, d) => UpdateCurrency(sender, d, Currency.LEVEL_GOLD);
            CurrencyManager.Instance.OnAirChange += (sender, d) => UpdateCurrency(sender, d, Currency.AIR);
            CurrencyManager.Instance.OnScoreChange += (sender, d) => UpdateCurrency(sender, d, Currency.SCORE);

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
            levelGamePanel.gameObject.SetActive(false);
            levelResultPanel.gameObject.SetActive(false);

            switch (panel)
            {
                case Panels.GAME:
                    levelGamePanel.gameObject.SetActive(true);
                    break;
                case Panels.RESULT:
                    levelResultPanel.gameObject.SetActive(true);
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