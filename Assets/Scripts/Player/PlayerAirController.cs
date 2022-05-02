using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAirController : MonoBehaviour
{
    [SerializeField] private float timeBetweenDrain = 1f;
    [SerializeField] private int drainAmount = 1;

    private void OnEnable()
    {
        GameManager.LevelManager.OnExit += Reset;
        StartCoroutine(AirTick());
    }

    private void OnDisable()
    {
        GameManager.LevelManager.OnExit -= Reset;
        StopCoroutine(AirTick());
    }

    void Reset()
    {
        CurrencyManager.Instance.SetCurrency(Currency.AIR, Player.Instance.GetMaxAirLevel());
    }

    IEnumerator AirTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenDrain);

            if (GameManager.Instance.GetState == GameState.PLAY)
            {
                CurrencyManager.Instance.AddCurrency(Currency.AIR, -drainAmount);
            }
        }
    }
}
