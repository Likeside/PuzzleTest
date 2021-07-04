using System;
using System.IO;
using UnityEngine;

namespace Scripts
{
    public class PanelManager: MonoBehaviour
    {
        [SerializeField] private GameObject _levelCompletePanel;
        [SerializeField] private GameObject _gameCompletePanel;


        private void Start()
        {
            _levelCompletePanel.SetActive(false);
            _gameCompletePanel.SetActive(false);
        }

        public void ShowLevelCompletePanel()
        {
            _levelCompletePanel.SetActive(!_levelCompletePanel.activeSelf);
        }

        public void ShowGameCompletePanel()
        {
            _gameCompletePanel.SetActive(!_gameCompletePanel.activeSelf);
        }
    }
}