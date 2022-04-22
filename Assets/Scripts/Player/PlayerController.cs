using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private FixedJoystick _fixedJoystick;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _moveSpeed;

    private void Start()
    {
        GameManager.instance.OnRestart += OnRestart;
        GameManager.instance.OnGameStateChange += OnStateChange;
    }

    private void OnDestroy()
    {
        GameManager.instance.OnRestart -= OnRestart;
        GameManager.instance.OnGameStateChange -= OnStateChange;
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.GetState == GameState.PLAY)
        {
            if (!_rigidbody2D.simulated)
                _rigidbody2D.simulated = true;
            
            _rigidbody2D.velocity =
                new Vector2(_fixedJoystick.Horizontal * _moveSpeed, _fixedJoystick.Vertical * _moveSpeed);
        }
        else
        {
            if (_rigidbody2D.simulated)
                _rigidbody2D.simulated = false;
        }
    }

    private void OnRestart()
    {
        _fixedJoystick.Reset();
    }

    private void OnStateChange(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.PLAY:
                _rigidbody2D.simulated = true;
                break;
            case GameState.PAUSE:
                _rigidbody2D.simulated = false;
                break;
        }
    }
}