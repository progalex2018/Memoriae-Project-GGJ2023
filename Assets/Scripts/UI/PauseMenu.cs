using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Memoriae.Control;
using Memoriae.Core;
using System;

namespace Manabi.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool GameIsPaused = false;

        [SerializeField] private GameObject pausedMenu;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private GameObject firstButton;

        private GameObject fpvCamera;

        private void Start()
        {
            pausedMenu.SetActive(false);
            fpvCamera = GameObject.FindGameObjectWithTag("FPVCamera");
            playerController.OnPlayerPaused += PlayerController_OnPlayerPause;
        }

        private void PlayerController_OnPlayerPause(object sender, EventArgs e)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        public void Resume()
        {
            fpvCamera.SetActive(true);
            pausedMenu.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
            playerController.enabled = true;
        }

        public void BackToMenu()
        {
            Time.timeScale = 1f;
            PersistentObjectSpawner persistentObj = FindObjectOfType<PersistentObjectSpawner>();
            persistentObj.DestroyPersistentObject();
            SceneManager.LoadScene(0);
        }

        private void Pause()
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButton);
            playerController.enabled = false;
            fpvCamera.SetActive(false);
            pausedMenu.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
    }
}
