using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;


    [Serializable]
    public class JSONLevelReader : MonoBehaviour
    {
        public TextAsset textJSON;
        public LevelData levelData;
        public List<int[,]> Parts = new List<int[,]>();

        [Serializable]
        public class LevelData
        {
            [JsonProperty("template")]
            public int[,] Template { get; set; }

            [JsonProperty("parts")]
            public int[][,] Parts { get; set; }
        }

        void Awake()
        {
            levelData = JsonConvert.DeserializeObject<LevelData>(textJSON.text);
            
            foreach (var list in levelData.Parts)
            {
                Parts.Add(list);
            }
        }
    }
