using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PigeonCounterUI : MonoBehaviour
    {
        public Slider Slider;

        private void Update()
        {
            Slider.value = Pigeon.DeathCount / RoundManager.Instance.CurrentMap.MaxKillablePigeons;
        }
    }
}