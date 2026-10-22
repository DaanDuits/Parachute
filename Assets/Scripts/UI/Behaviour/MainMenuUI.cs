using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace FoGD.UI.Behaviour
{
    public class MainMenuUI : UIBehaviour
    {
        private void Awake()
        {
            Root.Q<Button>("play-button").clicked += LoadLevel;
            Root.Q<Button>("quit-button").clicked += Quit;
        }

        private void LoadLevel()
        {
            SceneManager.LoadScene(1);
        }
        private void Quit()
        {
            Application.Quit();
        }
    }
}
