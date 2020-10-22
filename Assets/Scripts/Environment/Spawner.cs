using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : ObjectPool
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _target;
    [SerializeField] private int _widthSpawner;

    private System.Random _random = new System.Random();
    private Vector3 _offset;
    int _nextSpawn = 1;

    private void Start()
    {
        _offset = new Vector3(_spawnPoint.position.x - _target.position.x, _spawnPoint.position.y, _spawnPoint.position.z);
        Initialize(_prefab);
    }

    private void Update()
    {
        _spawnPoint.position = new Vector3(_target.position.x, _offset.y, _offset.z);

        if (_spawnPoint.transform.position.x >= _nextSpawn)
        {
            int pointX = (int)_spawnPoint.transform.position.x;
            _nextSpawn = _random.Next(pointX + 2, pointX + 5);

            GenerateNewBoulders();

            DisableObjectAbroadScreen();
        }
    }

    private void GenerateNewBoulders()
    {
        int generatesInOneCycle = _random.Next(0, (int)(_widthSpawner * 0.2f));
        HashSet<int> positions = new HashSet<int>();
        positions = FillPositions(positions, generatesInOneCycle);

        foreach (int item in positions)
        {
            if (TryGetObject(out GameObject boulder))
            {
                boulder.SetActive(true);
                _spawnPoint.position = new Vector3(_target.position.x + _offset.x, _offset.y, _offset.z + item);
                boulder.transform.position = _spawnPoint.position;
            }
        }
    }

    private HashSet<int> FillPositions(HashSet<int> positions, int count)
    {
        HashSet<int> timeHashSet = new HashSet<int>();
        for (int i = 0; i < count; i++)
        {
            int elem = GetRandomPlace();
            while (positions.Contains(elem))
                elem = GetRandomPlace();

            timeHashSet.Add(elem);
        }

        return timeHashSet;
    }

    private int GetRandomPlace()
    {
        return Random.Range(-(_widthSpawner / 2), _widthSpawner / 2);
    }
}
