using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace FoGD.UI.Behaviour
{
    public class WinLoseScreenUI : UIBehaviour
    {
        [SerializeField] private ThemeStyleSheet winTheme, loseTheme;
        [SerializeField] private string winText = "You Won", loseText = "You Lost";

        private void Awake()
        {
            Document.enabled = false;
        }

        private void Retry()
        {
            SceneManager.LoadScene(1);
        }

        private void MainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void Lose()
        {
            Document.enabled = true;
            SetCallbacks();
            Root.Q<Label>("condition-text").text = loseText;
            Document.panelSettings.themeStyleSheet = loseTheme;
        }

        public void Win()
        {
            Document.enabled = true;
            SetCallbacks();
            Root.Q<Label>("condition-text").text = winText;
            Document.panelSettings.themeStyleSheet = winTheme;
        }

        private void SetCallbacks()
        {
            Root.Q<Button>("retry-button").clicked += Retry;
            Root.Q<Button>("main-menu-button").clicked += MainMenu;
        }
    }
}
