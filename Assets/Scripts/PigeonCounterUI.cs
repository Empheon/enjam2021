using System;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PigeonCounterUI : MonoBehaviour
    {
        public Slider Slider;
        public TextMeshProUGUI PigeonText;

        private void Update()
        {
            Slider.value = (float) Pigeon.DeathCount / RoundManager.Instance.CurrentMap.MaxKillablePigeons;
            PigeonText.text = Pigeon.DeathCount.ToString();
        }
    }
}