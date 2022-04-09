using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class ChunkSpawner : MonoBehaviour
    {
        [SerializeField][Range(0, 50)] private int poolSize = 20;
        [SerializeField] private GameObject[] chunkPrefabs;
        
        private GameObject[] _chunkPool;

        private void Awake()
        {
            
        }

        private void Start()
        {
            PopulatePool(ref _chunkPool, chunkPrefabs);
            Spawn();
        }
        
        public void Spawn()
        {
            Debug.Log($"Try spawn chunk");
            EnableObjectInPool(_chunkPool);
        }
        
        public void PopulatePool(ref GameObject[] pool, GameObject[] prefabs)
        {
            var currentPlayerSkin = Player.instance.GetCurrentSkin();
            pool = new GameObject[poolSize];

            List<GameObject> tempPrefabs = new List<GameObject>();

            foreach (var prefab in prefabs)
            {
                var chunkSkin = prefab.GetComponent<Chunk>().GetChunkSkin();
                if (chunkSkin == currentPlayerSkin || chunkSkin == PlayerSkin.ALL)
                {
                    tempPrefabs.Add(prefab);
                }
            }
            
            for (int i = 0; i < poolSize; i++)
            {
                var prefab = tempPrefabs[Random.Range(0, tempPrefabs.Count)];
                pool[i] = Instantiate(prefab, transform);
                pool[i].SetActive(false);
            }
        }
        
        private void EnableObjectInPool(GameObject[] pool)
        {
            foreach (var o in pool)
            {
                if (!o.activeSelf)
                {
                    o.SetActive(true);
                    return;
                }
            }
        }
    }
}