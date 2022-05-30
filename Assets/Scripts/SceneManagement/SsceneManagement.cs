using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace SceneManagementt
{
    public class SsceneManagement : MonoBehaviour
    {
        [SerializeField] int _sceneToBeLoaded = 0;

        Keyboard kb;

        private void Awake()
        {
            kb = InputSystem.GetDevice<Keyboard>();
        }

        private void Update()
        {
            if(kb.rKey.isPressed)
            {
                RestartLevel();
            }
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(_sceneToBeLoaded);
        }

        public void QuitButton()
        {
            Application.Quit();
        }
    }
}
