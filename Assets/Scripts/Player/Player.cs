using Managers;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerAirController))]
public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private float startingXPosition;
    [SerializeField] private float startingYPosition;
    [SerializeField] private int airLevel = 100;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } 
        else if (Instance != this)
        {
            Destroy (gameObject);
        }
        
        startingXPosition = transform.position.x;
        startingYPosition = transform.position.y;
    }

    private void OnEnable()
    {
        GameManager.LevelManager.OnExit += PositionReset;
    }

    private void OnDisable()
    {
        GameManager.LevelManager.OnExit -= PositionReset;
    }

    private void PositionReset()
    {
        transform.position = new Vector3(startingXPosition, startingYPosition);
    }
    
    

    public int GetMaxAirLevel()
    {
        return airLevel;
    }
}