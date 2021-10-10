using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class StartCountdown : MonoBehaviour
    {
        public TextMeshProUGUI Three;
        public TextMeshProUGUI Two;
        public TextMeshProUGUI One;
        public TextMeshProUGUI Go;

        private Color m_baseColor;
        private Color transparent;
        private float interval = 0.5f;
        private float fromScale = 5;

        private void Start()
        {
            m_baseColor = Three.color;
            transparent = m_baseColor;
            transparent.a = 0;
            
            Three.color = transparent;
            Two.color = transparent;
            One.color = transparent;
            Go.color = transparent;
        }

        public void StartSequence()
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(Three.transform.DOScale(1, interval).From(fromScale).SetEase(Ease.InCubic));
            seq.Join(Three.DOColor(m_baseColor, interval / 2f));
            seq.Append(Three.DOColor(transparent, interval / 2f));
            seq.Join(Two.transform.DOScale(1, interval).From(fromScale).SetEase(Ease.InCubic));
            seq.Join(Two.DOColor(m_baseColor, interval / 2f));
            seq.Append(Two.DOColor(transparent, interval / 2f));
            seq.Join(One.transform.DOScale(1, interval).From(fromScale).SetEase(Ease.InCubic));
            seq.Join(One.DOColor(m_baseColor, interval / 2f));
            seq.Append(One.DOColor(transparent, interval / 2f));
            seq.Join(Go.transform.DOScale(1, interval).From(fromScale).SetEase(Ease.InCubic));
            seq.Join(Go.DOColor(m_baseColor, interval / 2f));
            seq.Append(Go.DOColor(transparent, interval / 2f));
        }
    }
}