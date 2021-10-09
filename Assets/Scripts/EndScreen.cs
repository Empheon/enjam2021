using UnityEngine;

namespace DefaultNamespace
{
    public class EndScreen : MonoBehaviour
    {
        public GameObject WinPanel;
        public GameObject LosePanel;
        
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
    }
}