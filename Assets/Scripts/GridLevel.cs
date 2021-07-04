using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts
{
    public sealed class GridLevel: GridBase
    {
        [SerializeField] public int levelIndex;
        public Action OnLevelComplete;
        private List<GridSquare> _gridSquaresMB = new List<GridSquare>();
        protected override void CreateGrid()
        {
            SpawnGridSquares(_level.levelData.Template, _square);
            SetGridSquarePositions(_level.levelData.Template);
            DeleteEmptySquares();
            foreach (var square in _gridSquares)
            {
                var gridSquare = square.GetComponent<GridSquare>();
                _gridSquaresMB.Add(gridSquare);
                gridSquare.SquareOccupied += CheckIfLevelComplete;
            }
            
            
        }

        private void CheckIfLevelComplete()
        {
            bool levelCompleted = _gridSquaresMB.All(s => s.Occupied);
            if (levelCompleted)
            {
                foreach (var gridSquare in _gridSquaresMB)
                {
                    gridSquare.SquareOccupied -= CheckIfLevelComplete;
                }
                OnLevelComplete?.Invoke();
            }
        }
        
    }
}