using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _chunkPrefab;
    [SerializeField] private Transform _player;
    
    private int _chunkLength;
    private Chunk[] _chunks = new Chunk[3];
    private int _currenIndex = 0;
    private Vector3 _lastPosition;
    private int _ratio = 3;

    private void Start()
    {
        for (int i = 0; i < _chunks.Length; i++)
        {
            var spawned = Instantiate(_chunkPrefab);
            _chunks[i] = spawned.GetComponent<Chunk>();
        }

        _chunkLength = _chunks[_currenIndex].ChunkLength;
        _lastPosition = _chunks[_currenIndex].MoveChunk(new Vector3(_player.position.x, 0, _player.position.z));
        _currenIndex++;
    }

    private void FixedUpdate()
    {
        if (_player.position.x > _lastPosition.x - (_chunkLength * _ratio))
        {
            if (_currenIndex < _chunks.Length)
            {
                MoveChunk(_currenIndex);
            }
            else
            {
                _currenIndex = 0;
                MoveChunk(_currenIndex);
            }
            _currenIndex++;
        }
    }

    private void MoveChunk(int index)
    {
        _lastPosition = _chunks[index].MoveChunk(_lastPosition);
    }
}
