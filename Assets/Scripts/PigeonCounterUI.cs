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

        public GameObject SliderMilestoneBar;
        public TextMeshProUGUI MilestoneText;

        private void Update()
        {
            Slider.value = (float) Pigeon.DeathCount / RoundManager.Instance.CurrentMap.MaxKillablePigeons;
            PigeonText.text = Pigeon.DeathCount.ToString();

            var pos = SliderMilestoneBar.transform.localPosition;
            pos.y = Mathf.Lerp(-195, 195, (float) RoundManager.Instance.CurrentMap.DeathCountNeeded 
                                          / RoundManager.Instance.CurrentMap.MaxKillablePigeons);
            SliderMilestoneBar.transform.localPosition = pos;

            MilestoneText.text = RoundManager.Instance.CurrentMap.DeathCountNeeded.ToString();
        }
    }
}