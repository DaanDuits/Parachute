using System.Collections;
using UnityEngine;
using FoGD.Obstacles.Factory;
using FoGD.UI.Behaviour;

namespace FoGD.Obstacles.Manager
{
    public class ObstacleManager : MonoBehaviour
    {
        [Header("Position Properties")]
        [SerializeField] private float spawnPosY = 5f;
        [SerializeField] private Vector2 xSpawnPosRange = new Vector2(-5f, 5f);

        [Header("Obstacle Properties")]
        [SerializeField] private float spawnTime = 2f;
        [Tooltip("All prefabs for the obstacle types")][SerializeField] private ObstacleFactory.Settings factorySettings;

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
                // Create a new obstacle and set it's properties
                FallingObstacle current = CreateObstacle();
                current.Context = this;
                current.transform.position = RandomStartPos();

                yield return new WaitForSeconds(spawnTime);

                // Set a new random obstacle type
                SetRandomObstacle();
            }
        }

        private Vector3 RandomStartPos()
        {
            return new Vector3(Random.Range(xSpawnPosRange.x, xSpawnPosRange.y), spawnPosY, 0);
        }

        private void SetRandomObstacle()
        {
            // Get a new random obstacle
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
