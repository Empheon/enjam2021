using UnityEngine;

namespace DefaultNamespace
{
    public class TrackHighlighter : MonoBehaviour
    {
        private Color m_baseColor;
        private bool m_mouseOver;

        private void Start()
        {
            m_baseColor = GetComponentInChildren<Renderer>().material.color;
        }

        private void OnMouseEnter()
        {
            m_mouseOver = true;
            GetComponentInChildren<Renderer>().material.color = m_baseColor * 1.5f;
        }

        private void OnMouseExit()
        {
            m_mouseOver = false;
            GetComponentInChildren<Renderer>().material.color = m_baseColor;
        }
        
        void Update()
        {
            if (m_mouseOver && Input.GetMouseButtonDown(0))
            {
                transform.parent.Rotate(Vector3.up, 90);
            }
        }
    }
}