using System.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    public class SaveLoadManager : MonoBehaviour
    {
        public static SaveLoadManager instance;
        
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
        
        public async Task Init()
        {
            
        }
    }
}