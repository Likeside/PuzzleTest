using System;
using UnityEngine;
using UnityEngine.EventSystems;

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


        private void OnCollisionExit2D(Collision2D other)
        {
            Occupied = false;
        }
    }
}