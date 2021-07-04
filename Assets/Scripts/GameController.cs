using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Scripts
{
    public class GameController: MonoBehaviour
    {
        [SerializeField] private GridLevel _level;
        [SerializeField] private PanelManager _panelManager;
        [SerializeField] private Text _levelText;
        [SerializeField] private string _topPanelText = "Уровень: ";
        private LevelTracker _levelTracker;
        
        private void Start()
        {
            _levelTracker = FindObjectOfType<LevelTracker>();
            _level.OnLevelComplete += CompleteLevel;
            _levelText.text = _topPanelText + _level.levelIndex;
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