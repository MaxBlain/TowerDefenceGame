using UnityEngine;
using System;

[Serializable]
public class Tower
{
    public string name;
    public int cost;
    public GameObject prefab;
    public float targetingRange;

    public Tower (string _name, int _cost, GameObject _prefab, float _targetingRange)
    {
        name = _name;
        cost = _cost;
        prefab = _prefab;
        targetingRange = _targetingRange;
    }
}
