using System;
using System.Collections.Generic;
using Managers;
using Spawners;
using UnityEngine;


public class Chunk : MonoBehaviour
{
    public event Action OnReset;
    
    [SerializeField] private PlayerSkin skin;
    [SerializeField] private float moveSpeed = 3f;

    private ChunkSpawner _chunkSpawner;
    private List<GameObject> _children = new List<GameObject>();

    private void Update()
    {
        if (GameManager.Instance.GetState == GameState.PLAY)
        {
            transform.position += Vector3.left * (Time.deltaTime * moveSpeed);
        }
    }

    public PlayerSkin GetChunkSkin()
    {
        return skin;
    }

    public void SetChunkSpawner(ChunkSpawner chunkSpawner)
    {
        _chunkSpawner = chunkSpawner;
    }

    public void SetChildren()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            _children.Add(child.gameObject);
        }
    }
    
    public void SpawnNext()
    {
        _chunkSpawner.Spawn();
    }

    public void Reset()
    {
        OnReset?.Invoke();
        
        foreach (GameObject child in _children)
        {
            child.SetActive(true);
        }
        
        gameObject.SetActive(false);
        transform.position = Vector3.zero;
    }
}
