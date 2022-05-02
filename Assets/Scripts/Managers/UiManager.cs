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

        [SerializeField] private Timer timer;

        [Header("Main")] [SerializeField] private Transform mainPanel;
        [SerializeField] private Transform gamePanel;
        [SerializeField] private TMP_Text goldText;
        [SerializeField] private Button menuStartButton;

        [Header("Level")] [SerializeField] private Transform levelGamePanel;
        [SerializeField] private TMP_Text levelScoreText;
        [SerializeField] private TMP_Text levelGoldText;
        [SerializeField] private TMP_Text levelTimeText;
        [SerializeField] private TMP_Text levelAirText;
        [SerializeField] private Button levelPauseButton;

        [Header("Level Pause")] [SerializeField]
        private Transform levelPausePanel;

        [SerializeField] private Button levelPauseSettingButton;
        [SerializeField] private Button levelPauseResumeButton;
        [SerializeField] private Button levelPauseExitButton;

        [Header("Level Settings")] [SerializeField]
        private Transform levelSettingPanel;

        [SerializeField] private Button levelPauseSettingCloseButton;

        [Header("Level Result")] [SerializeField]
        private Transform levelResultPanel;

        [SerializeField] private TMP_Text levelResultScoreText;
        [SerializeField] private TMP_Text levelResultGoldText;
        [SerializeField] private Button levelResultClaimButton;
        [SerializeField] private Button levelResultClaimX2Button;

        void Awake()
        {
            if (Instance is null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            if (mainPanel is not null && gamePanel is not null)
            {
                mainPanel.gameObject.SetActive(true);
                gamePanel.gameObject.SetActive(false);
            }
        }

        public async Task Init()
        {
            #region [MAIN SCREEN]

            if (menuStartButton is not null)
                menuStartButton.onClick.AddListener(() =>
                {
                    CurrencyManager.Instance.SetCurrency(Currency.SCORE, 0);
                    CurrencyManager.Instance.SetCurrency(Currency.LEVEL_GOLD, 0);
                    mainPanel.gameObject.SetActive(false);
                    gamePanel.gameObject.SetActive(true);
                    GameManager.LevelManager.StartLevel();
                });

            #endregion

            #region [GAME SCREEN]

            #region LEVEL

            if (levelPauseButton is not null)
                levelPauseButton.onClick.AddListener(() =>
                {
                    GameManager.LevelManager.PauseLevel();
                    levelPausePanel.gameObject.SetActive(true);
                });

            #endregion

            #region LEVEL PAUSE

            if (levelPauseSettingButton is not null)
                levelPauseSettingButton.onClick.AddListener(() => { levelSettingPanel.gameObject.SetActive(true); });
            
            if (levelPauseResumeButton is not null)
                levelPauseResumeButton.onClick.AddListener(() =>
                {
                    levelPausePanel.gameObject.SetActive(false);
                    GameManager.LevelManager.StartLevel();
                });
            
            if (levelPauseExitButton is not null)
                levelPauseExitButton.onClick.AddListener(ShowLevelResult);

            #endregion

            #region LEVEL SETTINGS

            if (levelPauseSettingCloseButton is not null)
                levelPauseSettingCloseButton.onClick.AddListener(() =>
                {
                    levelSettingPanel.gameObject.SetActive(false);
                });

            #endregion

            #region LEVEL RESULT

            if (levelResultClaimButton is not null)
                levelResultClaimButton.onClick.AddListener(() =>
                {
                    GameManager.LevelManager.ExitLevel();
                });
            
            if (levelResultClaimX2Button is not null)
                levelResultClaimX2Button.onClick.AddListener(() =>
                {
                    CurrencyManager.Instance.AddCurrency(Currency.GOLD, CurrencyManager.Instance.GetCurrency(Currency.LEVEL_GOLD));
                    GameManager.LevelManager.ExitLevel();
                });


            #endregion

            #endregion
        }

        public async Task Subscribe()
        {
            CurrencyManager.Instance.OnGoldChange += (sender, d) => UpdateCurrency(sender, d, Currency.GOLD);
            CurrencyManager.Instance.OnLevelGoldChange += (sender, d) => UpdateCurrency(sender, d, Currency.LEVEL_GOLD);
            CurrencyManager.Instance.OnAirChange += (sender, d) => UpdateCurrency(sender, d, Currency.AIR);
            CurrencyManager.Instance.OnScoreChange += (sender, d) => UpdateCurrency(sender, d, Currency.SCORE);

            if (timer != null && levelTimeText != null)
            {
                timer.OnTimerChange += (sender, time) =>
                {
                    TimeSpan timeSpan = TimeSpan.FromSeconds(time);
                    levelTimeText.text = $"Time: {timeSpan:m\\:ss}";
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
                        goldText.text = $"{value}";
                    break;
                case Currency.LEVEL_GOLD:
                    if (levelGoldText != null)
                        levelGoldText.text = $"Gold: {value}";
                    
                    if (levelResultGoldText != null)
                        levelResultGoldText.text = $"+ {value}";
                    break;
                case Currency.AIR:
                    if (levelAirText != null)
                        levelAirText.text = $"Air: {value}";
                    break;
                case Currency.SCORE:
                    if (levelScoreText != null)
                        levelScoreText.text = $"Score: {value}";
                    
                    if (levelResultScoreText != null)
                        levelResultScoreText.text = $"{value}";
                    break;
            }
        }

        public void ShowLevelResult()
        {
            SetPanel(Panels.RESULT);
        }

        public void ShowMainScreen()
        {
            levelPausePanel.gameObject.SetActive(false);
            levelResultPanel.gameObject.SetActive(false);
            levelSettingPanel.gameObject.SetActive(false);
            levelGamePanel.gameObject.SetActive(true);
            
            gamePanel.gameObject.SetActive(false);
            mainPanel.gameObject.SetActive(true);
        }
    }

    public enum Panels
    {
        GAME,
        RESULT
    }
}