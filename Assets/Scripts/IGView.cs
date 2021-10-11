using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class IGView : MonoBehaviour
    {
        public GameObject StartButton;
        public GameObject PigeonCounter;

        public GameObject CountdownCircle;
        public GameObject CountdownText;

        private void Start()
        {
            PigeonCounter.SetActive(false);
            CountdownText.SetActive(false);
            
            RoundManager.Instance.OnGameReload += () =>
            {
                StartButton.SetActive(true);
                PigeonCounter.SetActive(false);
                CountdownCircle.SetActive(true);
                CountdownText.SetActive(false);
            };
            RoundManager.Instance.OnTrainDeparted += () =>
            {
                StartButton.SetActive(false);
                PigeonCounter.SetActive(true);
                CountdownCircle.SetActive(false);
                CountdownText.SetActive(true);
            };
        }
    }
}