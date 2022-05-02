using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TargetSystem))]
public class RotateToTarget : MonoBehaviour
{
    [SerializeField] private bool isRotate = true;
    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed = 25;

    private TargetSystem _targetSystem;
    private Vector2 _direction;

    private void Awake()
    {
        _targetSystem = GetComponent<TargetSystem>();
    }
    
    private void OnEnable()
    {
        _targetSystem.OnChangeTarget += SetTarget;
    }

    private void OnDisable()
    {
        _targetSystem.OnChangeTarget -= SetTarget;
    }

    private void Update()
    {
        if (isRotate && target != null)
        {
            _direction = target.position - transform.position;
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
    
    private void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
