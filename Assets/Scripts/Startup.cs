using Managers;
using UnityEngine;

public static class Startup
{
    private static GameObject _root;
    
    //[RuntimeInitializeOnLoadMethod]
    public static void Execute()
    {
        _root = GameObject.Find("SCRIPTS");

        if (_root == null)
        {
            _root = new GameObject("SCRIPTS");
        }

        CreateObject<SaveLoadManager>();
        CreateObject<SettingsManager>();
        CreateObject<CurrencyManager>();
        CreateObject<UiManager>();
        
        CreateObject<GameManager>();
        
        Debug.Log("<b>Startup</b> finished");
        
        GameManager.Instance.StartLevel();
    }

    private static void CreateObject<T>() where T : MonoBehaviour
    {
        if(Object.FindObjectOfType<T>() != null) return;

        var obj = new GameObject(typeof(T).Name);
        obj.AddComponent<T>();
        
        if(_root != null)
            obj.transform.SetParent(_root.transform);
    }
}