using System.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    public class SaveLoadManager : MonoBehaviour
    {
        public static SaveLoadManager Instance;
        
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
    }
}