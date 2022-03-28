using System;
using UnityEngine;

namespace Managers
{
    public class CurrencyManager : MonoBehaviour
    {
        public static CurrencyManager instance;
        
        public EventHandler<float> OnScoreChange;
        public EventHandler<float> OnMaxScoreChange;

        [SerializeField] private int coins = 0;
        [SerializeField] private float score = 0;
        [SerializeField] private float maxScore = 0;
        
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

        public int GetCoins => coins;
        public float GetScore => score;
        public float GetMaxScore => maxScore;

        public void SetScore(float score)
        {
            this.score += score;

            if (this.score > this.maxScore)
            {
                maxScore = this.score;
                OnMaxScoreChange?.Invoke(this, this.score);
            }
            
            OnScoreChange?.Invoke(this, this.score);
        }
    }
}