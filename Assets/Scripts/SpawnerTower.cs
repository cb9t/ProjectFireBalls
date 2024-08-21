using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTower : MonoBehaviour
{
    [SerializeField] private GameObject[] _spawnList;
    [SerializeField] private float _distancePositionY;

    private void Start()
    {
        var spawnPositionY = 0f;
        for (int i = 0;  i < _spawnList.Length; i++)
        {
            var spawnPoint = new Vector3(0f, spawnPositionY, 0f);
            Instantiate(_spawnList[i], spawnPoint, Quaternion.identity, gameObject.transform);
            spawnPositionY += _distancePositionY;
        }
    }
}
