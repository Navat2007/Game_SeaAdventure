using System;
using UnityEngine;

[RequireComponent(typeof(TargetSystem))]
public class MoveToTarget : MonoBehaviour
{
    [SerializeField] private bool isMove = true;
    [SerializeField] private bool keepDistance = true;
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float distanceToTarget = 2f;
    
    private TargetSystem _targetSystem;

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
        if (isMove && target != null)
        {
            if (Vector2.Distance(target.position, transform.position) > distanceToTarget)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    target.position,
                    moveSpeed * Time.deltaTime);
            }
            else if(keepDistance)
            {
                /*
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    target.position,
                    moveSpeed * Time.deltaTime);
                    */
            }
            
        }
    }

    private void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
