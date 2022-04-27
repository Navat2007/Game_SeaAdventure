using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _offset;

    private Vector2 _startPosition;
    private float _newXPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (GameManager.Instance.GetState == GameState.PLAY)
        {
            _newXPosition = Mathf.Repeat(Time.time * -_speed, _offset);

            transform.position = _startPosition + Vector2.right * _newXPosition;
        }
    }
}
