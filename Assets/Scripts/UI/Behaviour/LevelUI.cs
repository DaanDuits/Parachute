using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace FoGD.UI.Behaviour
{
    public class LevelUI : UIBehaviour
    {
        [SerializeField] private string scoreName = "Score";

        private Label _scoreLabel;
        private string _scoreText;

        private VisualElement _healthHolder;

        private void Awake()
        {
            // Initialize values
            _scoreText = scoreName + ": ";
            _scoreLabel = Root.Q<Label>("score-text");

            _healthHolder = Root.Q<VisualElement>("health-holder");
        }

        private void Start()
        {
            GameManager.Instance.OnScoreChanged.AddListener(SetScoreText);
            GameManager.Instance.OnHealthChanged.AddListener(SetHealthIcons);
        }

        public void SetScoreText(int score)
        {
            // Update the label to the correct score
            _scoreLabel.text = _scoreText + score;  
        }

        public void SetHealthIcons(int health)
        {
            int currentHealth = _healthHolder.childCount;
            int diff = health - currentHealth;

            if (diff < 0)
            {
                for (int i = diff; i < 0; i++)
                {
                    _healthHolder.RemoveAt(0);
                }
            }
            else
            {
                for (int i = 0; i < diff; i++)
                {
                    _healthHolder.Add(CreateHealthIcon());
                }
            }
        }

        private VisualElement CreateHealthIcon()
        {
            VisualElement icon = new VisualElement();
            icon.AddToClassList("health-icon");

            return icon;
        }
    }
}
