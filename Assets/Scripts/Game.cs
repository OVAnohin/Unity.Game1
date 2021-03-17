using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _chunkPrefab;
    [SerializeField] private Transform _player;
    [SerializeField] private ConfigurationData _configuration;

    private Chunk[] _chunks = new Chunk[3];
    private int _currenIndex = 0;
    private Vector3 _lastPosition;

    private void Start()
    {
        for (int i = 0; i < _chunks.Length; i++)
        {
            var spawned = Instantiate(_chunkPrefab);
            _chunks[i] = spawned.GetComponent<Chunk>();
        }

        _lastPosition = _chunks[_currenIndex].ResetChunk(new Vector3(_player.position.x, 0, _player.position.z));
        _currenIndex++;
    }

    private void Update()
    {
        if (_player.position.x > _lastPosition.x - (_configuration.ChunkLength * 3))
        {
            if (_currenIndex < _chunks.Length)
                _lastPosition = _chunks[_currenIndex].ResetChunk(_lastPosition);
            else
            {
                _currenIndex = 0;
                _lastPosition = _chunks[_currenIndex].ResetChunk(_lastPosition);
            }
            _currenIndex++;
        }
    }
}
