using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class CountdownUI : MonoBehaviour
    {
        public Image Circle;
        public TextMeshProUGUI TimerText;

        private void Start()
        {
            RoundManager.Instance.OnGameReload += () => gameObject.SetActive(true);
            RoundManager.Instance.OnTrainDeparted += () => gameObject.SetActive(false);
        }

        private void Update()
        {
            TimerText.text = Mathf.Ceil(RoundManager.Instance.CurrentMap.CountdownTimer) + "s";
            Circle.fillAmount = 1 - (RoundManager.Instance.CurrentMap.CountdownTimer /
                                RoundManager.Instance.CurrentMap.CountdownDuration);
        }
    }
}