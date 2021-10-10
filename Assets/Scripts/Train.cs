using System;
using PathCreation;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DefaultNamespace
{
    public class Train : MonoBehaviour
    {
        public float Speed = 5f;

        private PathCreator m_currentPath;

        private float pathDirFactor = 1;
        private float distanceTravelled;

        private bool m_isMoving;
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void StartMoving()
        {
            m_isMoving = true;
            audioSource.Play();
        }

        private void Update()
        {
            if (!m_isMoving)
            {
                return;
            }

            distanceTravelled += Time.deltaTime * Speed * pathDirFactor;
            transform.position = m_currentPath.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            transform.rotation = m_currentPath.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            if (pathDirFactor < 0)
            {
                transform.Rotate(Vector3.up, 180);
            }

            if (pathDirFactor < 0 && distanceTravelled < 0.01f ||
                pathDirFactor > 0 && distanceTravelled > m_currentPath.path.length - 0.01f)
            {
                var hits = Physics.SphereCastAll(transform.position, 0.1f,
                    transform.forward, 0.01f);

                bool trackFound = false;
                foreach (var hit in hits)
                {
                    if (hit.collider.gameObject.TryGetComponent(out Track hitTrack))
                    {
                        if (hitTrack.Path != m_currentPath)
                        {
                            AttachToNewTrack(hitTrack);
                            trackFound = true;
                            break;
                        }
                    }
                }

                if (!trackFound && !RoundManager.Instance.IsEndScreenOpened)
                {
                    m_isMoving = false;
                    EndScreen.Instance.SetReason("You crashed!");
                    RoundManager.Instance.Lose();
                }
            }
        }

        public void AttachToNewTrack(Track track)
        {
            m_currentPath = track.Path;

            transform.position = m_currentPath.path.GetClosestPointOnPath(transform.position);
            distanceTravelled = m_currentPath.path.GetClosestDistanceAlongPath(transform.position);

            if (distanceTravelled > 0.5)
            {
                pathDirFactor = -1;
            }
            else
            {
                pathDirFactor = 1;
            }

            if (track.IsDestination)
            {
                RoundManager.Instance.DestinationReached();
            }
        }
    }
}