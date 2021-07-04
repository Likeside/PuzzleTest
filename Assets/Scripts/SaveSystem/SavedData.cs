using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.SaveSystem
{
    [Serializable]
    public class SavedData
    {
        public float[][] listOfFigurePositions;
        
        public SavedData()
        {
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