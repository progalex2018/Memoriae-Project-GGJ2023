using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Memoriae.Scene
{
    public class BackToMenu : MonoBehaviour
    {
        public void GetBackToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
