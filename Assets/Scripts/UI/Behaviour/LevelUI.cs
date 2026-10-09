using UnityEngine;
using UnityEngine.UIElements;

namespace FoGD.UI.Behaviour
{
    public class LevelUI : UIBehaviour
    {
        [SerializeField] private string scoreName = "Score";

        private Label _scoreLabel;
        private string _scoreText;
        private int _score = 0;

        private void Awake()
        {
            // Initialize values
            _scoreText = scoreName + ": ";
            _scoreLabel = Root.Q<Label>("score-text");
            UpdateScore();
        }

        private void UpdateScore()
        {
            // Update the label to the correct score
            _scoreLabel.text = _scoreText + _score;
        }

        /// <summary>
        /// Add amount to the current score
        /// </summary>
        /// <param name="amount">The amount added to the score</param>
        public void AddScore(int amount)
        {
            _score += amount;
            UpdateScore();
        }

        /// <summary>
        /// Subtract amount from the current score
        /// </summary>
        /// <param name="amount">The amount subtracted from the score</param>
        public void RemoveScore(int amount)
        {
            _score -= amount;
            UpdateScore();
        }
    }
}
