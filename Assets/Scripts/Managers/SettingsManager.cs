using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    }
    
    public async Task Init()
    {
        
    }
    
    public async Task Subscribe()
    {
        
    }

    public bool GetIsReady()
    {
        return true;
    }
}
