using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridObject", menuName = "GridObject/GridObject", order = 51)]
public class GridObjectData : ScriptableObject
{
    [SerializeField] private GridLayer _layer;
    [SerializeField] private int _chance;
    [SerializeField] private Material _material;
    [SerializeField] private string _nickName;

    public GridLayer Layer => _layer;
    public Material Material => _material;
    public string NickName => _nickName;
    public int Chance
    {
        get
        {
            _chance = Mathf.Clamp(_chance, 1, 100);
            return _chance;
        }
    }
}
