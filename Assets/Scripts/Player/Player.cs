using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _startingXPosition;
    [SerializeField] private float _startingYPosition;
    
    private void Awake()
    {
        _startingXPosition = transform.position.x;
        _startingYPosition = transform.position.y;
    }

    private void Start()
    {
        GameManager.instance.OnRestart += PositionReset;
    }

    private void OnDestroy()
    {
        GameManager.instance.OnRestart -= PositionReset;
    }

    private void PositionReset()
    {
        transform.position = new Vector3(_startingXPosition, _startingYPosition);
    }
}
