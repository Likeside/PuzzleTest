using System;
using UnityEngine;

namespace Scripts
{
    public class GridSquare: MonoBehaviour
    {
        public bool Occupied { get; set; }
        public Action SquareOccupied;

        public void OccupySquare()
        {
            Occupied = true;
            SquareOccupied?.Invoke();
        }

        public void DeOccupySquare()
        {
            Occupied = false;
        }
        
    }
}