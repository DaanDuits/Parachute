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
            _scoreText = scoreName + ":";
            _scoreLabel = Root.Q<Label>("score-text");
            _scoreLabel.text = _scoreText;
        }

        /// <summary>
        /// Add amount to the current score
        /// </summary>
        /// <param name="amount">The amount added to the score</param>
        public void AddScore(int amount)
        {
            _score += amount;
            _scoreLabel.text = _scoreText + " " + _score;
        }

        /// <summary>
        /// Subtract amount from the current score
        /// </summary>
        /// <param name="amount">The amount subtracted from the score</param>
        public void RemoveScore(int amount)
        {
            _score -= amount;
            _scoreLabel.text = _scoreText + " " + _score;
        }
    }
}
