using UnityEngine;
using UnityEngine.UIElements;

namespace FoGD.UI.Behaviour
{
    public class LevelUI : UIBehaviour
    {
        [SerializeField] private string scoreName = "Score";

        private Label _scoreLabel;
        private string _scoreText;

        private void Awake()
        {
            // Initialize values
            _scoreText = scoreName + ": ";
            _scoreLabel = Root.Q<Label>("score-text");
        }

        public void SetScoreText(int score)
        {
            // Update the label to the correct score
            _scoreLabel.text = _scoreText + score;  
        }
    }
}
