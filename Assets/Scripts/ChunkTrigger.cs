using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChunkTrigger : MonoBehaviour
{
    private enum ChunkTriggerType
    {
        RESET,
        SPAWN
    }

    [SerializeField] private ChunkTriggerType chunkTriggerType;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.TryGetComponent(out Chunk chunk))
        {
            if (chunkTriggerType == ChunkTriggerType.SPAWN)
            {
                chunk.SpawnNext();
            }
            
            if (chunkTriggerType == ChunkTriggerType.RESET)
            {
                chunk.Reset();
            }
        }
    }
}
