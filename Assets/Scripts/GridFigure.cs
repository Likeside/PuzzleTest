using System;
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
        private RectTransform _rectTransform;
        private Vector3 _defaultScale;
        private Vector3 _defaultPosition;

        private void Awake()
        {
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
            Debug.Log(groupCenter);
            
            foreach (var square in _gridSquares)
            {
                square.transform.localPosition -= offset;
            }
        }


        #region Имплементация интерфейсов для драг'н'дропа
        
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("PointerDown");
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("BeginDrag");
            transform.localScale = new Vector3(_levelGrid.squareScale/squareScale, _levelGrid.squareScale/squareScale, _levelGrid.squareScale/squareScale);

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("EndDrag");
            transform.localScale = _defaultScale;
            transform.localPosition = _defaultPosition; //добавить логику
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta/ _canvas.scaleFactor;
        }
        
        
        #endregion
    }
}