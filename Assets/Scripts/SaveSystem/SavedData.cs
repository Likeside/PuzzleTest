using System;
using UnityEngine;

namespace Scripts.SaveSystem
{
    [Serializable]
    public class SavedData
    {
        public int levelsCompleted;
        public float[][] listOfFigurePositions;
        
        public SavedData()
        {
            levelsCompleted = GameObject.FindObjectOfType<LevelTracker>().LevelsCompleted;

            var allFigures = GameObject.FindObjectsOfType<GridFigure>();
            listOfFigurePositions = new float[allFigures.Length][];
            
            for (int i = 0; i < allFigures.Length; i++)
            {
                var xPos = allFigures[i].GetComponent<RectTransform>().anchoredPosition.x;
                var yPos = allFigures[i].GetComponent<RectTransform>().anchoredPosition.y;
                listOfFigurePositions[allFigures[i]._levelPartIndex] = new float[] {xPos, yPos};
            }
        }
     }
}