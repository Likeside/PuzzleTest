using UnityEngine;

namespace Scripts
{
    public class LevelTracker: MonoBehaviour
    {
        public int LevelsCompleted { get; set; }

        private void Awake()
        {
            LevelTracker[] objs = FindObjectsOfType<LevelTracker>();

            if (objs.Length > 1)
            {
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(this);
        }
        void Start()
        {
            LevelsCompleted = SaveSystem.SaveSystem.LoadGame().levelsCompleted;
        }
    }
}