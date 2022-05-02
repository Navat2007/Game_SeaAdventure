using System;
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
        
        private List<GameObject> _chunkPool = new List<GameObject>();

        private void OnEnable()
        {
            if (GameManager.LevelManager.GetNeedRefillChunks)
            {
                _chunkPool.Clear();
                
                foreach (var chunk in _chunkPool)
                {
                    Destroy(chunk.gameObject);
                }
                
                PopulatePool(_chunkPool, chunkPrefabs);
            }
            
            Spawn();
        }
        
        public void Spawn()
        {
            EnableObjectInPool(_chunkPool, GetRandomDisabledObjectInPool());
        }
        
        public void PopulatePool(List<GameObject> pool, GameObject[] prefabs)
        {
            var currentPlayerSkin = GameManager.PlayerManager.GetCurrentSkin();

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
                var chunkGameObject = Instantiate(prefab, transform);
                chunkGameObject.SetActive(false);
                chunkGameObject.GetComponent<Chunk>().SetChunkSpawner(this);
                chunkGameObject.GetComponent<Chunk>().SetChildren();
                pool.Add(chunkGameObject);
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
        
        private void EnableObjectInPool(List<GameObject> pool, int index)
        {
            pool[index].SetActive(true);
        }

        private int GetRandomDisabledObjectInPool()
        {
            int index = -1;
            while (index == -1)
            {
                var randomIndex = Random.Range(0, _chunkPool.Count);
                if (!_chunkPool[randomIndex].activeSelf)
                {
                    index = randomIndex;
                }
            }
            return index;
        }
    }
}