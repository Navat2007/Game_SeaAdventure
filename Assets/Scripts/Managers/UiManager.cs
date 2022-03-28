using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager instance;
        
        [SerializeField] private Transform _menuPanel;
        [SerializeField] private Transform _gamePanel;
        [SerializeField] private Transform _resultPanel;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _hightScoreText;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _restartButton;
 
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
 
            DontDestroyOnLoad (gameObject);
        }

        public void Init()
        {
            if(_startButton != null)
                _startButton.onClick.AddListener(() =>
                {
                    SetPanel(Panels.GAME);
                });
        
            if(_restartButton != null)
                _restartButton.onClick.AddListener(() =>
                {
                    //OnRestart?.Invoke(null, null);
                    //Score = 0;
                    SetPanel(Panels.GAME);
                });
        }
        
        public void SetPanel(Panels panel)
        {
            _menuPanel.gameObject.SetActive(false);
            _gamePanel.gameObject.SetActive(false);
            _resultPanel.gameObject.SetActive(false);

            switch (panel)
            {
                case Panels.MENU:
                    _menuPanel.gameObject.SetActive(true);
                    break;
                case Panels.GAME:
                    _gamePanel.gameObject.SetActive(true);
                    break;
                case Panels.RESULT:
                    _resultPanel.gameObject.SetActive(true);
                    break;
            }
        }

        public void ShowLevelResult()
        {
            _scoreText.SetText($"Your score: {CurrencyManager.instance.GetScore}");
            _hightScoreText.SetText($"Your best score: {CurrencyManager.instance.GetMaxScore}");
        
            SetPanel(Panels.RESULT);
        }
    }
    
    public enum Panels
    {
        MENU,
        GAME,
        RESULT
    }
}