using UnityEngine;
using FoGD.UI.Behaviour;

namespace FoGD
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelUI levelUI;

        private int _score = 0;

        public static GameManager Instance 
        { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            levelUI.SetScoreText(_score);
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
    }
}
