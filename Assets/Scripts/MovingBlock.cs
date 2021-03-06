using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [SerializeField] private bool _randomY = true;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _startingXPosition;
    [SerializeField] private float _startingYPosition;

    private void Start()
    {
        GameManager.LevelManager.OnExit += PositionReset;
        
        _startingXPosition = transform.position.x;
        _startingYPosition = transform.position.y;
    }
    
    private void OnDestroy()
    {
        GameManager.LevelManager.OnExit -= PositionReset;
    }

    private void Update()
    {
        if (GameManager.Instance.GetState == GameState.PLAY)
        {
            transform.position += Vector3.left * (Time.deltaTime * _moveSpeed);

            if (transform.position.x <= -15)
            {
                transform.position += Vector3.right * 30f;

                if(_randomY)
                {
                    float newY = _startingYPosition + UnityEngine.Random.Range(-1f, 1f);
                    transform.position = new Vector3(transform.position.x, newY);
                }
                
                gameObject.SetActive(false);
            }
        }
    }
    
    private void PositionReset()
    {
        transform.position = new Vector3(_startingXPosition, _startingYPosition);
        gameObject.SetActive(false);
    }
}
