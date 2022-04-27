using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Managers
{
    public class WebManager : MonoBehaviour
    {
        public static WebManager Instance;
        
        [SerializeField] Slider progressBar;
        [SerializeField] Text sliderValue;
        
        void Awake () 
        {
            if (Instance is null)
            {
                Instance = this;
            } 
            else if (Instance != this)
            {
                Destroy (gameObject);
            }
        }
        
        public IEnumerator GetRequest(string url)
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(url);
            yield return webRequest.SendWebRequest();
            if (webRequest.result is UnityWebRequest.Result.ProtocolError or UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                Debug.Log("GET: Request success");
                string result = webRequest.downloadHandler.text;
                Debug.Log(result);
                
                //get the picture binary stream by DATA
                //byte[] data = uwr.downloadHandler.data;
            }
        }
        
        public IEnumerator PostRequest(string url, Dictionary<string, string> data)
        {
            WWWForm form = new WWWForm();

            foreach (var item in data)
            {
                form.AddField(item.Key, item.Value, Encoding.UTF8);
            }
            
            UnityWebRequest webRequest = UnityWebRequest.Post(url, form);

            yield return webRequest.SendWebRequest();
            
            if (webRequest.result is UnityWebRequest.Result.ProtocolError or UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                Debug.Log("Sent successfully");
            }
        }
        
        IEnumerator DownloadFile(string url)
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(url);
            
            yield return webRequest.SendWebRequest();
            
            if (webRequest.result is UnityWebRequest.Result.ProtocolError or UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                while (!webRequest.isDone) 
                {
                    progressBar.value = webRequest.downloadProgress;
                    sliderValue.text = Math.Floor(webRequest.downloadProgress * 100) + "%";
                    yield return 0;
                }

                if (webRequest.isDone)
                {
                    progressBar.value = 1;
                    sliderValue.text = 100 + "%";
                }

                byte[] results = webRequest.downloadHandler.data;
                CreateFile(Application.streamingAssetsPath + "/MP4/test.mp4", results, webRequest.downloadHandler.data.Length); 
                AssetDatabase.Refresh(); 
            }
        }
        
        void CreateFile(string path, byte[] bytes, int length)
        {
            Stream stream;
            FileInfo file = new FileInfo(path);
            if (!file.Exists)
            {
                stream = file.Create();
            }
            else
            {
                return;
            }

            stream.Write(bytes, 0, length);
            stream.Close();
            stream.Dispose();
        }
    }
}
