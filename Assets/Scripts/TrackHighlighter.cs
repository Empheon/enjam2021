using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class TrackHighlighter : MonoBehaviour
    {
        private bool m_mouseOver;

        private Dictionary<Renderer, Color> m_baseColors;
        private bool m_isTurning;

        private void Start()
        {
            m_baseColors = new Dictionary<Renderer, Color>();
            
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                m_baseColors.Add(renderer, renderer.material.color);
            }
        }

        private void OnMouseEnter()
        {
            if (RoundManager.Instance.IsTrainDeparted || !RoundManager.Instance.CurrentMap.IsCountingDown)
            {
                return;
            }
            
            m_mouseOver = true;
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.material.color = m_baseColors[renderer] * 1.5f;
            }
        }

        private void OnMouseExit()
        {
            m_mouseOver = false;
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.material.color = m_baseColors[renderer];
            }
        }
        
        void Update()
        {
            if (RoundManager.Instance.IsTrainDeparted || !RoundManager.Instance.CurrentMap.IsCountingDown)
            {
                return;
            }
            
            if (m_mouseOver && Input.GetMouseButtonDown(0) && !m_isTurning)
            {
                m_isTurning = true;
                transform.parent.DORotate(transform.parent.rotation.eulerAngles + Vector3.up * 90
                    , 0.2f).SetEase(Ease.OutBack).OnComplete(() => m_isTurning = false);
                // transform.parent.Rotate(Vector3.up, 90);
            }
        }
    }
}