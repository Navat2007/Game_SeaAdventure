using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private Vector2 jumpVelocity;
    [SerializeField] private float _startingXPosition;
    [SerializeField] private float _startingYPosition;
    
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        _startingXPosition = transform.position.x;
        _startingYPosition = transform.position.y;
        
        GameManager.instance.OnRestart += (sender, args) =>
        {
            transform.position = new Vector3(_startingXPosition, _startingYPosition);
        };
    }

    void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            _rigidbody2D.velocity = jumpVelocity;
        }
    }
}
