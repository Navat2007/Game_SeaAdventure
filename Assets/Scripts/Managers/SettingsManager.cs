using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;
 
    void Awake () 
    {
        if (instance == null)
        {
            instance = this;
        } 
        else if (instance != this)
        {
            Destroy (gameObject);
        }
 
        DontDestroyOnLoad (gameObject);
    }
    
    public void Init()
    {
        
    }

    public bool GetIsReady()
    {
        return true;
    }
}
