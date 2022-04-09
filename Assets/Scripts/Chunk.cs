using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private PlayerSkin skin;

    public PlayerSkin GetChunkSkin()
    {
        return skin;
    }
}
