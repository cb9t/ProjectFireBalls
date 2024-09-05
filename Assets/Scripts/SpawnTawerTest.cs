using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTawerTest : MonoBehaviour
{
    [SerializeField] private TowerSpawner _towerSpawner;
    void Start()
    {
        _towerSpawner.SpawnTower(gameObject.transform);
    }

    
}
