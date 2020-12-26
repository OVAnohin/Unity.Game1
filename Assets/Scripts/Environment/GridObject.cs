using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    [SerializeField] private GridLayer _layer;
    [SerializeField] private int _chance;
    [SerializeField] private int _capacity;

    public GridLayer Layer => _layer;
    public int Capacity => _capacity;
    public int Chance => _chance;

    private void OnValidate()
    {
        _chance = Mathf.Clamp(_chance, 1, 100);
    }
}
