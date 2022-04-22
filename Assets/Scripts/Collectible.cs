using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

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
                CurrencyManager.instance.AddCurrency(Currency.LEVEL_GOLD, pointsToAdd);
            
            if(collectibleType == CollectibleType.AIR_BUBLE)
                CurrencyManager.instance.AddCurrency(Currency.AIR, pointsToAdd);
        }
    }
}

public enum CollectibleType
{
    GOLD_COIN,
    AIR_BUBLE
}