using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Memoriae.UI
{
    public class MenuController : MonoBehaviour
    {

        [SerializeField] private GameObject controlMenu;

        private bool isActive = false;

        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void ControlMenu()
        {
            if (!isActive)
            {
                controlMenu.SetActive(true);
                isActive = true;
            }
            else
            {
                controlMenu.SetActive(false);
                isActive = false;
            }
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
