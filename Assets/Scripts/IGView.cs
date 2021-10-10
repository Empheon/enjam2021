using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class IGView : MonoBehaviour
    {
        public GameObject StartButton;
        public GameObject SliderBar;

        public GameObject CountdownCircle;
        public GameObject CountdownText;

        private void Start()
        {
            SliderBar.SetActive(false);
            CountdownText.SetActive(false);
            
            RoundManager.Instance.OnGameReload += () =>
            {
                StartButton.SetActive(true);
                SliderBar.SetActive(false);
                CountdownCircle.SetActive(true);
                CountdownText.SetActive(false);
            };
            RoundManager.Instance.OnTrainDeparted += () =>
            {
                StartButton.SetActive(false);
                SliderBar.SetActive(true);
                CountdownCircle.SetActive(false);
                CountdownText.SetActive(true);
            };
        }
    }
}