using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class SceneLoader: MonoBehaviour
    {
        private PanelManager _panelManager;
        private LevelTracker _levelTracker;
        

        private void Start()
        {
            _panelManager = FindObjectOfType<PanelManager>();
            _levelTracker = FindObjectOfType<LevelTracker>();
        } 

        public void LoadNextLevel()
        {
            if (_levelTracker.LevelsCompleted + 1 >= SceneManager.sceneCountInBuildSettings)
            {
                _panelManager.ShowGameCompletePanel();
            }
            else
            {
                SceneManager.LoadScene(_levelTracker.LevelsCompleted + 1);
            }
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}