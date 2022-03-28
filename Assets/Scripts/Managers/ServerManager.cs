using UnityEngine;

namespace Managers
{
    public class ServerManager : MonoBehaviour
    {
        public static ServerManager instance;
    
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

        public void Login()
        {
        
        }
    
        public void Registration()
        {
        
        }

        public void SendScore()
        {
        
        }
    }
}
