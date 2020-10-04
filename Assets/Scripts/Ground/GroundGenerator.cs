using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _groundPrefab;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private GameObject _boulderPrefab;
    [SerializeField] private Transform _groundPool;
    [SerializeField] private Sphere _player;
    [SerializeField] private int _x;
    [SerializeField] private int _z;
    [SerializeField] private int _coin;
    [SerializeField] private int _boulder;

    private int _startX;
    private int _startZ;
    private int _nextStep = 1;
    private Queue<GameObject> _grounds = new Queue<GameObject>();
    private Queue<GameObject> _layerOnGrounds = new Queue<GameObject>();
    private int _deltaX;
    private int _lastX;

    private void Start()
    {
        SetupVariables();

        HashSet<int> positionsCoin = new HashSet<int>();
        positionsCoin = FillPosition(positionsCoin, _coin);
        HashSet<int>  positionsBoulder = FillPosition(positionsCoin, _boulder);

        int i = 0;
        for (int x = _startX; x < _x; x++)
            for (int z = _startZ; z < _z; z++)
            {
                _grounds.Enqueue(GenerateGround(_groundPrefab, x, 0, z));

                if (positionsCoin.Contains(i))
                    _layerOnGrounds.Enqueue(GenerateGround(_coinPrefab, x, 1, z));

                if (positionsBoulder.Contains(i))
                    _layerOnGrounds.Enqueue(GenerateGround(_boulderPrefab, x, 1, z));

                i += 1;
            }
    }

    private GameObject GenerateGround(GameObject prefab, int x, int y, int z)
    {
        GameObject spawned = Instantiate(prefab, _groundPool);
        spawned.transform.position = new Vector3(x, _groundPool.position.y + y, z);
        return spawned;
    }

    private void SetupVariables()
    {
        _startX = (_x / 3) * -1;
        _startZ = (_z / 3) * -1;
        _x += _startX;
        _z += _startZ;
        _deltaX = _x + (-_startX);
        _lastX = _x;
    }

    private HashSet<int> FillPosition(HashSet<int> positions, int count)
    {
        HashSet<int> timeHashSet = new HashSet<int>();
        for (int i = 0; i < count; i++)
        {
            int elem = Random.Range(0, _deltaX * (_z + (-_startZ)));
            while (positions.Contains(elem))
                elem = Random.Range(0, _deltaX * (_z + (-_startZ)));

            timeHashSet.Add(elem);
        }

        return timeHashSet;
    }

    private void Update()
    {
        if (_player.transform.position.x >= _nextStep)
        {
            _nextStep += 1;

            while ((_grounds.Peek().transform.position.x * -1) + _lastX == _deltaX)
            {
                GameObject ground = _grounds.Dequeue();
                ground.transform.position = new Vector3(_lastX, ground.transform.position.y, ground.transform.position.z);
                _grounds.Enqueue(ground);
            }

            HashSet<int> positions = new HashSet<int>();
            while ((_layerOnGrounds.Peek().transform.position.x * -1) + _lastX == _deltaX)
            {
                GameObject element = _layerOnGrounds.Dequeue();

                int zPosition = Random.Range(_startZ, _z);
                while (positions.Contains(zPosition))
                    zPosition = Random.Range(_startZ, _z);

                positions.Add(zPosition);
                element.transform.position = new Vector3(_lastX, element.transform.position.y, zPosition);
                element.SetActive(true);
                _layerOnGrounds.Enqueue(element);
            }

            _lastX += 1;
        }
    }

}
