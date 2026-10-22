using UnityEngine;
using FoGD.UI.Behaviour;

namespace FoGD
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int maxScore = 5000;
        [Space]
        [SerializeField] private int maxHealth = 3;
        [SerializeField] private int startHealth = 3;
        [SerializeField] private Transform playerTransform;
        [Space]
        [SerializeField] private LevelUI levelUI;

        private int _score = 0;

        private int _health = 0;

        public static GameManager Instance 
        { get; private set; }

        public float ScorePercentage
        {
            get
            {
                return _score / (float)maxScore;
            }
        }
        public float HealthPercentage
        {
            get
            {
                return (_health - 1) / (float)maxHealth;
            }
        }
        public Vector3 PlayerPosition
        { get => playerTransform.position; }

        private void Awake()
        {
            // Setup singleton
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            AddHealth(startHealth);

            levelUI.SetScoreText(_score);
            levelUI.SetHealthIcons(_health);
        }

        /// <summary>
        /// Add amount to the current score
        /// </summary>
        /// <param name="amount">The amount added to the score</param>
        public void AddScore(int amount)
        {
            _score += amount;
            levelUI.SetScoreText(_score);
        }
        /// <summary>
        /// Subtract amount from the current score
        /// </summary>
        /// <param name="amount">The amount subtracted from the score</param>
        public void RemoveScore(int amount)
        {
            _score -= amount;
            levelUI.SetScoreText(_score);
        }

        /// <summary>
        /// Add amount to the current health value
        /// </summary>
        /// <param name="amount">The amount added to the health value</param>
        public void AddHealth(int amount)
        {
            _health += amount;
            _health = Mathf.Clamp(_health, 0, maxHealth);
            levelUI.SetHealthIcons(_health);
        }
        /// <summary>
        /// Subtract amount from the current health value
        /// </summary>
        /// <param name="amount">The amount subtracted from the health value</param>
        public void RemoveHealth(int amount)
        {
            _health -= amount;
            _health = Mathf.Clamp(_health, 0, maxHealth);
            levelUI.SetHealthIcons(_health);
        }
    }
}
