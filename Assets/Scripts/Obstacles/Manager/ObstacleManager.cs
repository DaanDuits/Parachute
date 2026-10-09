using System.Collections;
using UnityEngine;
using FoGD.Obstacles.Factory;

namespace FoGD.Obstacles.Manager
{
    /// <summary>
    /// MonoBehaviour class that handles spawning new Obstacles
    /// </summary>
    public class ObstacleManager : MonoBehaviour
    {
        [Header("Position Properties")]
        [SerializeField] private float spawnPosY = 5f;
        [SerializeField] private float minSpawnPosX = -5f, maxSpawnPosX = 5f;

        [Header("Obstacle Properties")]
        [SerializeField] private float spawnTime = 2f;
        [SerializeField] private ObstacleFactory.Settings factorySettings;

        private FallingObstacle _currentObstacle;
        private ObstacleFactory _factory;

        private void Awake()
        {
            _factory = new ObstacleFactory(factorySettings);

            SetRandomObstacle();
            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                FallingObstacle current = CreateObstacle();
                current.Context = this;
                current.transform.position = RandomStartPos();

                yield return new WaitForSeconds(spawnTime);

                SetRandomObstacle();
            }
        }

        private Vector3 RandomStartPos()
        {
            return new Vector3(Random.Range(minSpawnPosX, maxSpawnPosX), spawnPosY, 0);
        }

        private void SetRandomObstacle()
        {
            int random = Random.Range(0, 3);
            switch (random)
            {
                case 0:
                    _currentObstacle = _factory.Parachutist();
                    break;
                case 1:
                    _currentObstacle = _factory.HealthCrate();
                    break;
                case 2:
                    _currentObstacle = _factory.DamageCrate();
                    break;
            }
        }

        private FallingObstacle CreateObstacle()
        {
            return Instantiate(_currentObstacle, transform);
        }
        public void DestroyObstacle(FallingObstacle target)
        {
            Destroy(target.gameObject);
        }
    }
}
