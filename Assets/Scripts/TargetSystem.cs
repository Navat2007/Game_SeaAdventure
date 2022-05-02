using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetSystem : MonoBehaviour
{
    public event Action<Transform> OnChangeTarget;

    [SerializeField] private Transform target;

    private void Start()
    {
#if ENABLE_INPUT_SYSTEM
        Vector3 mousePosition = Mouse.current.position.ReadValue();
#else
        Vector3 mousePosition = Input.mousePosition;
#endif
        var player = Player.Instance.transform;
        
        SetTarget(player);
    }

    public Transform GetTarget()
    {
        return target;
    }
    
    private void SetTarget(Transform newTarget)
    {
        target = newTarget;
        OnChangeTarget?.Invoke(target);
    }
}
