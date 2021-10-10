using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject CreditWrapper;

        private void Start()
        {
            CloseCredits();
        }

        public void StartGame()
        {
            SceneManager.LoadScene("MainScene");
        }

        public void OpenCredits()
        {
            CreditWrapper.SetActive(true);
        }

        public void CloseCredits()
        {
            CreditWrapper.SetActive(false);
        }
    }
}