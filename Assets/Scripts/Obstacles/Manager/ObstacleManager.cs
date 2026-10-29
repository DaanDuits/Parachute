using System.Collections;
using UnityEngine;
using FoGD.Obstacles.Bank;

namespace FoGD.Obstacles.Manager
{
    public class ObstacleManager : MonoBehaviour
    {
        [Header("Position Properties")]
        [SerializeField] private float spawnPosY = 5f;
        [SerializeField] private Vector2 xSpawnPosRange = new Vector2(-5f, 5f);

        [Header("Obstacle Properties")]
        [SerializeField] private float spawnSpeed = 2f;
        [SerializeField] private float startSpawnTime = 2f;
        [Tooltip("All prefabs for the obstacle types")][SerializeField] private ObstacleBank.Settings factorySettings;
        [Space]
        [Tooltip("The maximum change of a damage crate spawning")][Range(0f, 1f)][SerializeField] private float damageCrateSpawnChange = 0.2f;
        [Space]
        [Tooltip("The maximum change of a health crate spawning")][Range(0f, 1f)][SerializeField] private float healthCrateSpawnChange = 0.1f;

        private float _spawnTime;

        private ObstacleBank _bank;

        private IEnumerator _activeCoroutine;

        private void Awake()
        {
            _bank = new ObstacleBank(factorySettings);

            _spawnTime = startSpawnTime;

            _activeCoroutine = SpawnLoop();
            StartCoroutine(_activeCoroutine);
        }
        private void Start()
        {
            GameManager.Instance.OnLose.AddListener(Stop);
            GameManager.Instance.OnWin.AddListener(Stop);
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                // Create a new obstacle and set it's properties
                FallingObstacle current = GetObstacle();
                current.Context = this;
                current.transform.position = RandomStartPos();

                yield return new WaitForSeconds(_spawnTime);
                _spawnTime = Mathf.PerlinNoise1D(Time.time) * spawnSpeed;
            }
        }

        private Vector3 RandomStartPos()
        {
            return new Vector3(Random.Range(xSpawnPosRange.x, xSpawnPosRange.y), spawnPosY, 0);
        }

        private FallingObstacle GetObstacle()
        {
            FallingObstacle obstacle = _bank.Parachutist();
            float random = Random.Range(0f, 1f);
            
            // Calculate the damage crate spawn change based on the score percentage using an ease in curve
            float damageCrateChange = Mathf.Clamp(Mathf.Pow(GameManager.Instance.ScorePercentage, 2f), 0f, damageCrateSpawnChange);

            // Calculate the health crate spawn change based on the inverted health percentage then use anpther ease in curve
            float healthInvert = 1f - GameManager.Instance.HealthPercentage;
            float healthCrateChange = damageCrateChange + Mathf.Clamp(Mathf.Pow(healthInvert, 2f) * healthCrateSpawnChange, 0f, healthCrateSpawnChange);

            // Set the correct obstacle
            if (random <= damageCrateChange)
                obstacle = _bank.DamageCrate();
            else if (random < healthCrateChange)
                obstacle = _bank.HealthCrate();

            return Instantiate(obstacle, transform);
        }

        public void DestroyObstacle(FallingObstacle target)
        {
            Destroy(target.gameObject);
        }

        /// <summary>
        /// Stop spawning obstacles
        /// </summary>
        public void Stop()
        {
            StopCoroutine(_activeCoroutine);
            
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
