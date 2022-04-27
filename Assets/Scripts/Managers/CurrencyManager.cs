using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    public class CurrencyManager : MonoBehaviour
    {
        public static CurrencyManager Instance;
        
        public EventHandler<int> OnGoldChange;
        public EventHandler<int> OnLevelGoldChange;
        public EventHandler<int> OnAirChange;
        public EventHandler<int> OnScoreChange;
        public EventHandler<int> OnMaxScoreChange;

        [SerializeField] private int scoreOnGoldPickup = 25;
        
        [SerializeField] private int gold = 0;
        [SerializeField] private int levelGold = 0;
        [SerializeField] private int air = 100;
        [SerializeField] private int score = 0;
        [SerializeField] private int maxScore = 0;

        void Awake () 
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
        
        public async Task Init()
        {
            
        }
        
        public int GetCurrency(Currency currency)
        {
            switch (currency)
            {
                case Currency.GOLD:
                    return gold;
                case Currency.LEVEL_GOLD:
                    return levelGold;
                case Currency.AIR:
                    return air;
                case Currency.SCORE:
                    return score;
                case Currency.MAX_SCORE:
                    return maxScore;
                default:
                    return 0;
            }
        }
        
        public void AddCurrency(Currency currency, int value)
        {
            switch (currency)
            {
                case Currency.GOLD:
                    gold += value;
                    OnGoldChange?.Invoke(null, gold);
                    break;
                case Currency.LEVEL_GOLD:
                    levelGold += value;
                    OnLevelGoldChange?.Invoke(null, levelGold);
                    AddCurrency(Currency.SCORE, scoreOnGoldPickup);
                    break;
                case Currency.AIR:
                    air += value;

                    if (air > 100)
                        air = 100;
                    
                    OnAirChange?.Invoke(null, air);
                    break;
                case Currency.SCORE:
                    score += value;
                    if (score > maxScore)
                    {
                        SetCurrency(Currency.MAX_SCORE, score);
                    }
                    OnScoreChange?.Invoke(null, score);
                    break;
                case Currency.MAX_SCORE:
                    maxScore += value;
                    OnMaxScoreChange?.Invoke(null, maxScore);
                    break;
            }
        }
    
        public void SetCurrency(Currency currency, int value)
        {
            switch (currency)
            {
                case Currency.GOLD:
                    gold = value;
                    OnGoldChange?.Invoke(null, gold);
                    break;
                case Currency.LEVEL_GOLD:
                    levelGold = value;
                    OnLevelGoldChange?.Invoke(null, levelGold);
                    break;
                case Currency.AIR:
                    air = value;
                    
                    if (air > 100)
                        air = 100;
                    
                    OnAirChange?.Invoke(null, air);
                    break;
                case Currency.SCORE:
                    score = value;
                    if (score > maxScore)
                    {
                        SetCurrency(Currency.MAX_SCORE, score);
                    }
                    OnScoreChange?.Invoke(null, score);
                    break;
                case Currency.MAX_SCORE:
                    maxScore = value;
                    OnMaxScoreChange?.Invoke(null, maxScore);
                    break;
            }
        }
        
        public void FullReset()
        {
            SetCurrency(Currency.GOLD, 0);
            SetCurrency(Currency.LEVEL_GOLD, 0);
            SetCurrency(Currency.AIR, 100);
            SetCurrency(Currency.SCORE, 0);
            SetCurrency(Currency.MAX_SCORE, 0);
        }
    }

    public enum Currency
    {
        GOLD,
        LEVEL_GOLD,
        AIR,
        SCORE,
        MAX_SCORE
    }
}