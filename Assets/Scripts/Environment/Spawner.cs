using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityRandom = UnityEngine.Random;
using SystemRandom = System.Random;
using System;

public abstract class Spawner : ObjectPool
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _target;
    [SerializeField] private int _widthSpawner;
    [SerializeField] private Transform _checkPoint;

    private Vector3 _offset;
    private int _nextCheckpoint = 1;
    private SystemRandom _systemRandom = new SystemRandom();

    private void Start()
    {
        _offset = _spawnPoint.position - _target.position;
        _checkPoint.position =  СalculateStartCheckPointPosition();
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
            DisableObjectAbroadScreen(_checkPoint.position.x);
        }
    }

    private Vector3 СalculateStartCheckPointPosition()
    {
        float angle = Camera.main.fieldOfView;
        float radiantAngle = (90 - angle) * (Mathf.PI / 180);
        float tangens = Mathf.Tan(radiantAngle);

        float y = Mathf.Abs(Camera.main.transform.position.z) * tangens;
        float x = -(((Screen.currentResolution.width * y) / Screen.currentResolution.height) + 1);

        return new Vector3(x, 0, 0);
    }

    private void SetNextCheckpoint(int pointX)
    {
        int randomOffset = UnityRandom.Range(2, 5);
        _nextCheckpoint = pointX + randomOffset;
    }

    private void SpawnNewObjects()
    {
        int numberOfObject = UnityRandom.Range(0, (int)(_widthSpawner * 0.2f));
        IEnumerable positions = CreatePositions(numberOfObject);

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

    private IEnumerable CreatePositions(int count)
    {
        int[] positions = new int[_widthSpawner];
        int[] newPositions = new int[count];

        for (int i = 0; i < _widthSpawner; i++)
            positions[i] = i - _widthSpawner / 2;

        Shuffle(_systemRandom, positions);
        Array.Copy(positions, newPositions, count);

        return newPositions;
    }

    private void Shuffle(SystemRandom rand, int[] positions)
    {
        int placeOld, placeNew;
        int element;

        for (int i = 0; i < positions.Length; i++)
        {
            placeOld = rand.Next(0, positions.Length);
            placeNew = rand.Next(0, positions.Length);
            element = positions[placeOld];
            positions[placeOld] = positions[placeNew];
            positions[placeNew] = element;
        }
    }
}
