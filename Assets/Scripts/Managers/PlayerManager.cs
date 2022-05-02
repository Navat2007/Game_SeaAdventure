using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerSkin
{
    ALL,
    DOLPHIN,
    SUBMARINE
}

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerSkin currentSkin = PlayerSkin.DOLPHIN;

    private void Awake()
    {
        GameManager.PlayerManager = this;
    }

    public PlayerSkin GetCurrentSkin()
    {
        return currentSkin;
    }
}
