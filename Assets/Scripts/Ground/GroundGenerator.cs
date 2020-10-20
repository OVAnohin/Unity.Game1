using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _groundPrefab;
    [SerializeField] private Transform _groundPool;
    [SerializeField] private Sphere _player;
    [SerializeField] private int _lengthPlayingField;
    [SerializeField] private int _widthPlayingField;

    private Queue<GameObject> _grounds = new Queue<GameObject>();
    private int _nextStep = 1;

    private void Start()
    {
        for (int x = -_lengthPlayingField / 2; x <= _lengthPlayingField / 2; x++)
            for (int z = -_widthPlayingField / 2; z < _widthPlayingField / 2; z++)
                _grounds.Enqueue(GenerateGround(_groundPrefab, x, z));
    }

    private void Update()
    {
        if (_player.transform.position.x >= _nextStep)
        {
            _nextStep += 1;
            MoveGround();
        }
    }

    private void MoveGround()
    {
        for (int i = 0; i < _widthPlayingField; i++)
        {
            GameObject element = _grounds.Dequeue();
            element.transform.position = element.transform.position + new Vector3(_lengthPlayingField + 1, 0, 0);
            _grounds.Enqueue(element);
        }
    }

    private GameObject GenerateGround(GameObject prefab, int x, int z)
    {
        GameObject spawned = Instantiate(prefab, _groundPool);
        spawned.transform.position = new Vector3(x, _groundPool.position.y, z);
        return spawned;
    }
}
