using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RotateToTarget : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 25;

    private Vector2 _direction;

    private void Update()
    {
#if ENABLE_INPUT_SYSTEM
        Vector3 mousePosition = Mouse.current.position.ReadValue();
#else
        Vector3 mousePosition = Input.mousePosition;
#endif
        _direction = Camera.main.ScreenToWorldPoint(mousePosition) - transform.position;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
