using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    [SerializeField] private GridLayer _layer;
    [SerializeField] private int _chance;
    [SerializeField] private string _nickName;

    public GridLayer Layer => _layer;
    public int Chance => _chance;
    public string NickName => _nickName;

    private void OnValidate()
    {
        _chance = Mathf.Clamp(_chance, 1, 100);
    }
}
