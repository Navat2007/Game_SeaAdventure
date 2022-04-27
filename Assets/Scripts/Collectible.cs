using System;
using Managers;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int pointsToAdd = 1;
    [SerializeField] private AudioClip coinTakeAudioClip;
    [SerializeField] private CollectibleType collectibleType;
    
    public event Action OnCollect;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<Player>();
            
            //player?.PlaySound(_coinTakeClip);
            
            gameObject.SetActive(false);
            OnCollect?.Invoke();
            
            if(collectibleType == CollectibleType.GOLD_COIN)
                CurrencyManager.Instance.AddCurrency(Currency.LEVEL_GOLD, pointsToAdd);
            
            if(collectibleType == CollectibleType.AIR_BUBLE)
                CurrencyManager.Instance.AddCurrency(Currency.AIR, pointsToAdd);
        }
    }
}

public enum CollectibleType
{
    GOLD_COIN,
    AIR_BUBLE
}