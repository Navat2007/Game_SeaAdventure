using System.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    public class SettingsManager : MonoBehaviour
    {
        public static SettingsManager Instance;
 
        void Awake () 
        {
            if (Instance == null)
            {
                Instance = this;
            } 
            else if (Instance != this)
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
}
