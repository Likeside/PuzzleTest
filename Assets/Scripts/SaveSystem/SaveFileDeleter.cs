using UnityEngine;

namespace Scripts.SaveSystem
{
    public class SaveFileDeleter: MonoBehaviour
    {
        
        public void DeleteSaveFile()
        {
            SaveSystem.ClearData();
            FindObjectOfType<LevelTracker>().LevelsCompleted = 0;
            
        }
    }
}