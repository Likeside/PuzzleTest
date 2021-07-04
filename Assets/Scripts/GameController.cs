using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Scripts
{
    public class GameController: MonoBehaviour
    {
        [SerializeField] private GridLevel _level;
        [SerializeField] private PanelManager _panelManager;
        private LevelTracker _levelTracker;
        
        private void Start()
        {
            _levelTracker = FindObjectOfType<LevelTracker>();
            _level.OnLevelComplete += CompleteLevel;
        }


        private void CompleteLevel()
        {
            _levelTracker.LevelsCompleted = _level.levelIndex;
           SaveSystem.SaveSystem.ClearData();
           var figures = FindObjectsOfType<GridFigure>();
           foreach (var figure in figures)
           {
               figure.ResetPosition();
           }
           SaveSystem.SaveSystem.SaveGame();
           if (_level.levelIndex == SceneManager.sceneCountInBuildSettings-1)
           {
               _panelManager.ShowGameCompletePanel();
           }
           else
           {
               _panelManager.ShowLevelCompletePanel();
           }
        }
    }
}