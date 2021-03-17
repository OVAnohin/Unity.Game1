using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Configuration", menuName = "Configuration/Configuration", order = 51)]
public class ConfigurationData : ScriptableObject
{
    [SerializeField] private int _chunkLength;
    [SerializeField] private int _chunkWidth;

    public int ChunkLength => _chunkLength;
    public int ChunkWidth => _chunkWidth;
}
