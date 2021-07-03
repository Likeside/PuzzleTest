using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridBase : MonoBehaviour
{
    [SerializeField] public float squareScale;
    [SerializeField] protected JSONLevelReader _level;
    [SerializeField] protected GameObject _square;
    
    private Vector2 _offset = Vector2.zero;
    private Vector2 _startPosition;
    protected List<GameObject> _gridSquares = new List<GameObject>();
    protected List<GameObject> _emptySquares = new List<GameObject>();

    private void Start()
    {
        CreateGrid();
    }

    protected virtual void CreateGrid()
    {
        SpawnGridSquares(_level.levelData.Template, _square);
        SetGridSquarePositions(_level.levelData.Template);
        DeleteEmptySquares();
    }

    //спавним квадраты, их количество равно количеству элементов в массиве, на месте нулей также добавляем квадраты в список пустых
    protected void SpawnGridSquares(int[,] squareArray, GameObject square)
    {
        for (int row = 0; row < squareArray.GetLength(0); ++row)
        {
            for (int column = 0; column < squareArray.GetLength(1); ++column)
            {
                if(squareArray[row, column] == 0 ) //проверяем, ноль или единица в массиве
                {
                    var empty = Instantiate(square);
                    _gridSquares.Add(empty);
                    _emptySquares.Add(empty);
                    
                }
                else
                {
                    _gridSquares.Add(Instantiate(square));
                }
                _gridSquares[_gridSquares.Count-1].transform.SetParent(transform); //делаем все квадраты дочерними объектами сетки
                _gridSquares[_gridSquares.Count - 1].transform.localScale =
                    new Vector3(squareScale, squareScale, squareScale);
            }
        }
    }
    
    //устанавливаем позицию квадратов
    protected void SetGridSquarePositions(int[,] squareArray)
    {
        int columnNumer = 0;
        int rowNumber = 0;

        var squareRect = _gridSquares[0].GetComponent<RectTransform>();
        
        //шаг смещения квадрата равен его ширине умноженной на скейл. 
        _offset.x = squareRect.rect.width * squareRect.transform.localScale.x; 
        _offset.y = squareRect.rect.height * squareRect.transform.localScale.y; 

        //стартовая позиция (позиция верхнего левого квадрата) "центрирует" квадраты относительно родителя (сетки)
        _startPosition.x = -(squareArray.GetLength(1) / 2) * (_offset.x) + _offset.x / 2f; 
        _startPosition.y = (squareArray.GetLength(0) / 2) * (_offset.y) - _offset.y / 2f;

        foreach (var square in _gridSquares)
        {
            
            //если номер столбца превышает количество столбцов в массиве, переходим на следующий ряд
            if (columnNumer+1 > squareArray.GetLength(1))
            {
                columnNumer = 0;
                rowNumber++;
            }

            //вычисляем расстояние, на которое нужно подвинуть квадрат (шаг смещения умноженное на колонку и ряд)
            var offsetPosX = _offset.x * columnNumer;
            var offsetPosY = _offset.y * rowNumber;
            

           square.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(_startPosition.x + offsetPosX, _startPosition.y - offsetPosY);
            square.GetComponent<RectTransform>().localPosition =
               new Vector3(_startPosition.x + offsetPosX, _startPosition.y - offsetPosY, 0);

            columnNumer++;
        }
    }

//удаляем пустые квадраты
    protected void DeleteEmptySquares()
    {
        if (_emptySquares.Any())
        {
            foreach (var square in _emptySquares)
            {
                _gridSquares.Remove(square);
                Destroy(square);
            }
        }
    }

   
}
