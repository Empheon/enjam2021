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

        private void Update()
        {
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
                
                foreach (var hit in hits)
                {
                    if (hit.collider.gameObject.TryGetComponent(out Track hitTrack))
                    {
                        if (hitTrack.Path != m_currentPath)
                        {
                            AttachToNewTrack(hitTrack);
                            break;
                        }
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.4f);
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
            Debug.Log(pathDirFactor + " " + distanceTravelled);
        }
    }
}