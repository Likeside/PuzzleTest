using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts
{
    public sealed class GridFigure: GridBase, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private GridBase _levelGrid;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private int _levelPartIndex;
        private List<FigureSquare> _figureSquares = new List<FigureSquare>();
        private RectTransform _rectTransform;
        private Vector3 _defaultScale;
        private Vector3 _defaultPosition;
        private bool _figurePlaced = false;

        private void Start()
        {
            CreateGrid();
            _rectTransform = GetComponent<RectTransform>();
            _defaultScale = _rectTransform.localScale;
        }

        //в фигуре вместо массива template передаем один из массивов в parts по индексу
        protected override void CreateGrid()
        {
            SpawnGridSquares(_level.Parts[_levelPartIndex], _square);
            SetGridSquarePositions(_level.Parts[_levelPartIndex]);
            DeleteEmptySquares();
            FixFigureYPosition();
            _defaultPosition = transform.localPosition;
            
            foreach (var square in _gridSquares)
            {
             _figureSquares.Add(square.GetComponent<FigureSquare>());   
            }
        }


        //после того, как удалили все пустые квадраты, центрируем их внутри фигуры
        private void FixFigureYPosition()
        {
            Vector3 sumVector = new Vector3(0f,0f,0f);
            
            foreach (var square in _gridSquares)
            {
                sumVector += square.transform.localPosition;
            }
            Vector3 groupCenter = sumVector / _gridSquares.Count;
            Vector3 offset = groupCenter - Vector3.zero;

            foreach (var square in _gridSquares)
            {
                square.transform.localPosition -= offset;
            }
        }


        #region Имплементация интерфейсов для драг'н'дропа
        
        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            
            if (!_figurePlaced)
            {
                transform.localScale = new Vector3(_levelGrid.squareScale / squareScale,
                    _levelGrid.squareScale / squareScale, _levelGrid.squareScale / squareScale);
            }

            foreach (var square in _figureSquares)
            {
                if (square.GridSquare != null)
                {
                    square.GridSquare.DeOccupySquare();
                    Debug.Log("Squares empty");
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {


            bool allCanBePlaced = true;
            foreach (var square in _figureSquares)
            {
                if (!square.HoveredOverEmptyGridSquare)
                {
                   allCanBePlaced = false;
                }
            }

            if (allCanBePlaced)
            {
                foreach (var square in _figureSquares)
                {
                    square.SetSquarePosOnGrid();
                    square.GridSquare.OccupySquare();
                    Debug.Log("Squares occupied");
                    _figurePlaced = true;
                }
            }
            else
            {
                _figurePlaced = false;
                transform.localPosition = _defaultPosition; 
                transform.localScale = _defaultScale;
            }
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta/ _canvas.scaleFactor;
        }
        
        
        #endregion
    }
}