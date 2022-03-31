using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager instance;
        
        [SerializeField] private Transform _gamePanel;
        [SerializeField] private Transform _resultPanel;
        [SerializeField] private TMP_Text _scoreText;
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
        }

        public async Task Init()
        {
            if(_restartButton != null)
                _restartButton.onClick.AddListener(() =>
                {
                    
                    CurrencyManager.instance.ResetScore();
                    SetPanel(Panels.GAME);
                    GameManager.instance.RestartLevel();
                });
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

        public void ShowGame()
        {
            SetPanel(Panels.GAME);
        }
        
        public void ShowLevelResult()
        {
            _scoreText.SetText($"Your score: {CurrencyManager.instance.GetScore}");
        
            SetPanel(Panels.RESULT);
        }
    }
    
    public enum Panels
    {
        GAME,
        RESULT
    }
}