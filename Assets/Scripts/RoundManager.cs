using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class RoundManager : MonoBehaviour
    {
        public static RoundManager Instance;

        [HideInInspector] public Action OnGameReload;
        [HideInInspector] public Action OnTrainDeparted;
        
        public List<MapGenerator> Levels;

        public EndScreen EndScreen;

        public StartCountdown StartCountdown;

        public Button StartButton;

        [HideInInspector]
        public bool IsTrainDeparted;
        
        private int m_currentLevel = 0;
        private MapGenerator m_currentMap;
        private bool m_endScreenOpened;

        public bool IsEndScreenOpened => m_endScreenOpened;
        public MapGenerator CurrentMap => m_currentMap;

        private void Awake()
        {
            Instance = this;
            EndScreen.HideAll();
        }

        private void Start()
        {
            ReloadLevel();
        }

        public void LoadNextLevel()
        {
            m_currentLevel++;
            ReloadLevel();
        }

        public void ReloadLevel()
        {
            if (m_currentMap)
            {
                Destroy(m_currentMap.gameObject);
            }

            StartButton.interactable = false;
            
            IsTrainDeparted = false;
            m_currentMap = Instantiate(Levels[m_currentLevel % Levels.Count]);
            EndScreen.HideAll();
            Pigeon.DeathCount = 0;
            m_endScreenOpened = false;
            
            OnGameReload?.Invoke();
            StartCountdown.StartSequence();
        }
        
        public void GoToMainMenu()
        {
            SceneManager.LoadScene("Scenes/MenuScene");
        }

        public void Win()
        {
            if (m_endScreenOpened)
            {
                return;
            }
            
            m_endScreenOpened = true;
            EndScreen.DisplayWinPanel();
        }

        public void Lose()
        {
            if (m_endScreenOpened)
            {
                return;
            }
            
            m_endScreenOpened = true;
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
                EndScreen.Instance.SetReason("Not enough pigeons sacrificed :(");
                Lose();
            }
        }

        public void StartGame()
        {
            OnTrainDeparted?.Invoke();
            m_currentMap.TrainInstance.StartMoving();
            IsTrainDeparted = true;
        }
    }
}