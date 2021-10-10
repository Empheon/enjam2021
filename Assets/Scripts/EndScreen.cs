using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class EndScreen : MonoBehaviour
    {
        public static EndScreen Instance;
        
        public GameObject WinPanel;
        public GameObject LosePanel;

        public TextMeshProUGUI Reason;

        private void Awake()
        {
            Instance = this;
        }

        public void HideAll()
        {
            WinPanel.SetActive(false);
            LosePanel.SetActive(false);
        }

        public void DisplayWinPanel()
        {
            WinPanel.SetActive(true);
        }

        public void DisplayLosePanel()
        {
            LosePanel.SetActive(true);
        }

        public void SetReason(string text)
        {
            Reason.text = text;
        }
    }
}