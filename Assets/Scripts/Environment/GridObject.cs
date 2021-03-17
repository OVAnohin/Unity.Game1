using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    [SerializeField] private GridObjectData _gridObject;

    public GridLayer Layer { get; private set; }
    public int Chance { get; private set; }
    public string NickName { get; private set; }

    public void Init()
    {
        Chance = _gridObject.Chance;
        Layer = _gridObject.Layer;
        GetComponent<MeshRenderer>().material = _gridObject.Material;
        NickName = _gridObject.NickName;
    }
}
