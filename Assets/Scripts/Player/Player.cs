using Managers;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerAirController))]
public class Player : MonoBehaviour
{
    public static Player Instance;
    
    [SerializeField] private PlayerSkin currentSkin = PlayerSkin.DOLPHIN;
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
        GameManager.Instance.OnExit += PositionReset;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnExit -= PositionReset;
    }

    private void PositionReset()
    {
        transform.position = new Vector3(startingXPosition, startingYPosition);
    }
    
    public PlayerSkin GetCurrentSkin()
    {
        return currentSkin;
    }

    public int GetMaxAirLevel()
    {
        return airLevel;
    }
}

public enum PlayerSkin
{
    ALL,
    DOLPHIN,
    SUBMARINE
}
