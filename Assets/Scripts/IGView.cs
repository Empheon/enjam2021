using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class IGView : MonoBehaviour
    {
        public GameObject StartButton;
        public GameObject SliderBar;

        private void Start()
        {
            SliderBar.SetActive(false);
            RoundManager.Instance.OnGameReload += () =>
            {
                StartButton.SetActive(true);
                SliderBar.SetActive(false);
            };
            RoundManager.Instance.OnTrainDeparted += () =>
            {
                StartButton.SetActive(false);
                SliderBar.SetActive(true);
            };
        }
    }
}