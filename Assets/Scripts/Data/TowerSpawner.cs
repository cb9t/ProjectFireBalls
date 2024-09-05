using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "TowerBuilder", menuName = "MyData/TowerBuilder", order = 1)]
    public class TowerSpawner : ScriptableObject
    {
        [SerializeField] private float _distancePositionY;
        [SerializeField] private GameObject _mainEmpty;

        [SerializeField] private TowerLVL[] _towerLVL;

        public void SpawnTower(Transform parent)
        {
            var rotateDelay = 0f;
            var spawnPositionY = 0f;

            for (int i = 0; i < _towerLVL.Length; i++)
            {
                var rotation = new Vector3(0f, rotateDelay, 0f);

                var lvl = _towerLVL[i];
                lvl.SpawnLVL(parent, spawnPositionY, _mainEmpty, rotation);

                spawnPositionY += _distancePositionY;
                rotateDelay += 15f;
            }
        }

        [System.Serializable]
        public struct TowerLVL
        {
            public GameObject TowerMain;
            public TowerPart[] TowerPart;

            public GameObject SpawnLVL(Transform parent, float spawnPositionY, GameObject Empty, Vector3 rotation)
            {
                var spawnPoint = new Vector3(0f, spawnPositionY, 0f);
                var mainEmpty = Instantiate(Empty, spawnPoint, Quaternion.identity, parent );
                Instantiate(TowerMain, mainEmpty.transform.position, Quaternion.identity, mainEmpty.transform);
                
                for (int i = 0; i < TowerPart.Length; i ++)
                {
                    var part = TowerPart[i];
                    part.SpawPart(mainEmpty.transform);
                }
                mainEmpty.transform.rotation = Quaternion.Euler(rotation);

                return mainEmpty;
            }
        }
        [System.Serializable]
        public struct TowerPart 
        {
            public RotationScript Part;
            public float RotateSpeed;
            public float RotatePosition;

            public GameObject SpawPart(Transform parent)
            {
                var spawnedPart = (Instantiate(Part, parent.position, Quaternion.Euler(0f, RotatePosition, 0f), parent));
                spawnedPart.SetSpeed(RotateSpeed);

                return spawnedPart.gameObject;
            }
        } 
    }
}
