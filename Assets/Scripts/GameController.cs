using System;
using System.Net.Mime;
using UnityEngine;

namespace Scripts
{
    public class GameController: MonoBehaviour
    {
        [SerializeField] private GridLevel _level;


        private void Start()
        {
            _level.OnLevelComplete += CompleteLevel;
        }


        private void CompleteLevel()
        {
            Debug.Log("Level Complete");
        }
    }
}