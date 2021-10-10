using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class CountdownUI : MonoBehaviour
    {
        public Image Circle;
        public TextMeshProUGUI TimerText;

        private bool m_isCountingDown;

        private AudioSource audioSource;
        private bool audioStarted;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            
            RoundManager.Instance.OnGameReload += () =>
            {
                audioStarted = false;
                gameObject.SetActive(true);
                StartCoroutine(StartCountdownDelayed());
            };
            RoundManager.Instance.OnTrainDeparted += () =>
            {
                gameObject.SetActive(false);
            };
        }

        private void Update()
        {
            TimerText.text = Mathf.Ceil(RoundManager.Instance.CurrentMap.CountdownTimer) + "s";
            Circle.fillAmount = 1 - (RoundManager.Instance.CurrentMap.CountdownTimer /
                                     RoundManager.Instance.CurrentMap.CountdownDuration);

            if (RoundManager.Instance.CurrentMap.CountdownTimer <= 5f && !audioStarted)
            {
                audioStarted = true;
                audioSource.Play();
            } 
        }
        
        
        IEnumerator StartCountdownDelayed()
        {
            yield return new WaitForSeconds(0.5f * 4);

            RoundManager.Instance.CurrentMap.IsCountingDown = true;
            CameraShake.Instance.Shake(0.2f, 0.1f);
            RoundManager.Instance.StartButton.interactable = true;
        }
    }
}