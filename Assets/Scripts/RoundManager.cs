using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class RoundManager : MonoBehaviour
    {
        public static RoundManager Instance;
        
        public List<MapGenerator> Levels;

        public EndScreen EndScreen;

        [HideInInspector]
        public bool IsTrainDeparted;
        
        private int m_currentLevel = 0;
        private MapGenerator m_currentMap;

        public MapGenerator CurrentMap => m_currentMap;

        private void Awake()
        {
            Instance = this;
            EndScreen.HideAll();
        }

        private void Start()
        {
            m_currentMap = Instantiate(Levels[0]);
        }

        public void LoadNextLevel()
        {
            m_currentLevel++;
            ReloadLevel();
        }

        public void ReloadLevel()
        {
            Destroy(m_currentMap.gameObject);
            IsTrainDeparted = false;
            m_currentMap = Instantiate(Levels[m_currentLevel % Levels.Count]);
            EndScreen.HideAll();
            Pigeon.DeathCount = 0;
        }

        public void Win()
        {
            EndScreen.DisplayWinPanel();
        }

        public void Lose()
        {
            EndScreen.DisplayLosePanel();
        }

        public void DestinationReached()
        {
            if (Pigeon.DeathCount >= m_currentMap.DeathCountNeeded)
            {
                Win();
            }
            else
            {
                Lose();
            }
        }

        public void StartGame()
        {
            m_currentMap.TrainInstance.StartMoving();
            IsTrainDeparted = true;
        }
    }
}