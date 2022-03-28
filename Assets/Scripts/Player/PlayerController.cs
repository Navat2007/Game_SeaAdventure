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

    private void OnEnable()
    {
        GameManager.instance.OnRestart += OnRestart;
    }

    private void OnDisable()
    {
        GameManager.instance.OnRestart -= OnRestart;
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.GetState == GameState.PLAY)
        {
            _rigidbody2D.velocity =
                new Vector2(_fixedJoystick.Horizontal * _moveSpeed, _fixedJoystick.Vertical * _moveSpeed);
        }
    }
    
    private void OnRestart(object sender, EventArgs e)
    {
        _fixedJoystick.enabled = false;
    }
}