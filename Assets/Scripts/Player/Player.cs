using Managers;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    
    [SerializeField] private PlayerSkin currentSkin = PlayerSkin.DOLPHIN;
    [SerializeField] private float startingXPosition;
    [SerializeField] private float startingYPosition;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
        else if (instance != this)
        {
            Destroy (gameObject);
        }
        
        startingXPosition = transform.position.x;
        startingYPosition = transform.position.y;
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
        transform.position = new Vector3(startingXPosition, startingYPosition);
    }
    
    public PlayerSkin GetCurrentSkin()
    {
        return currentSkin;
    }
}

public enum PlayerSkin
{
    ALL,
    DOLPHIN,
    SUBMARINE
}
