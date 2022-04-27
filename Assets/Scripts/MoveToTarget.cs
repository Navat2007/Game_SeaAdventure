using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToTarget : MonoBehaviour
{
    [SerializeField] private bool isMove = true;
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed = 5f;

    private void Update()
    {
        if (isMove && target != null)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                target.position,
                moveSpeed * Time.deltaTime);
        }
    }
}
