using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

public abstract class Spawner : ObjectPool
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _target;
    [SerializeField] private int _widthSpawner;

    private Vector3 _offset;
    private int _nextCheckpoint = 1;

    private void Start()
    {
        _offset = _spawnPoint.position - _target.position;
        Initialize(_prefab);
    }

    private void Update()
    {
        _spawnPoint.position = _target.position + _offset;

        if (_spawnPoint.position.x >= _nextCheckpoint)
        {
            int pointX = (int)_spawnPoint.position.x;
            SetNextCheckpoint(pointX);

            SpawnNewObjects();
            DisableObjectAbroadScreen();
        }
    }

    private void SetNextCheckpoint(int pointX)
    {
        int randomOffset = UnityRandom.Range(2, 5);
        _nextCheckpoint = pointX + randomOffset;
    }

    private void SpawnNewObjects()
    {
        int elementsInOneCycle = UnityRandom.Range(0, (int)(_widthSpawner * 0.2f));
        HashSet<int> positions = new HashSet<int>();
        positions = CreatingUniquePositions(positions, elementsInOneCycle);

        foreach (int item in positions)
        {
            if (TryGetObject(out GameObject element))
            {
                element.SetActive(true);
                _spawnPoint.position = new Vector3(_target.position.x + _offset.x, _offset.y, _offset.z + item);
                element.transform.position = _spawnPoint.position;
            }
        }
    }

    private HashSet<int> CreatingUniquePositions(HashSet<int> positions, int count)
    {
        HashSet<int> timeHashSet = new HashSet<int>();
        for (int i = 0; i < count; i++)
        {
            int place = GetRandomPlace();
            while (positions.Contains(place))
                place = GetRandomPlace();

            timeHashSet.Add(place);
        }

        return timeHashSet;
    }

    private int GetRandomPlace()
    {
        return UnityRandom.Range(-(_widthSpawner / 2), _widthSpawner / 2);
    }
}
