using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts
{
    public class FigureSquare: MonoBehaviour
    {
        public GridSquare GridSquare { get; set; }

        public bool HoveredOverEmptyGridSquare { get; private set; }
        private Vector3 _defaultPos;
        private Vector3 _posOnGrid;

        private void Start()
        {
            _defaultPos = transform.localPosition;
        }

        public void SetSquarePosOnGrid()
        {
            transform.position = _posOnGrid;
        }

        private void ResetSquarePos()
        {
            transform.localPosition = _defaultPos;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out GridSquare gridSquare))
            {
                if (!gridSquare.Occupied)
                {
                    HoveredOverEmptyGridSquare = true;
                    _posOnGrid = other.gameObject.transform.position;
                    GridSquare = gridSquare;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            HoveredOverEmptyGridSquare = false;
            GridSquare = null;
            ResetSquarePos();
        }
    }
}