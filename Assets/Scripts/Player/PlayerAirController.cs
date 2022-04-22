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

    private void Start()
    {
        StartCoroutine(AirTick());
    }

    IEnumerator AirTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenDrain);

            if (GameManager.instance.GetState == GameState.PLAY)
            {
                CurrencyManager.instance.AddCurrency(Currency.AIR, -drainAmount);
            }
        }
    }
}
