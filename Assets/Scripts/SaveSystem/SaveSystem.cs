using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32.SafeHandles;
using UnityEngine;

namespace Scripts.SaveSystem
{
    public class SaveSystem
    {
        private static string _path = Application.persistentDataPath + "/saveddata1.sd";
        
        public static void SaveGame()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(_path, FileMode.OpenOrCreate);

            SavedData savedData = new SavedData();
            
            formatter.Serialize(stream, savedData);
            stream.Close();
        }

        public static SavedData LoadGame()
        {
            if (File.Exists(_path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(_path, FileMode.Open);
                SavedData savedData = formatter.Deserialize(stream) as SavedData;
                stream.Close();
                return savedData;
            }
            else
            {
                SaveGame();
                return LoadGame();
            }
        }

        public static void ClearData()
        {
            File.Delete(_path);
        }
    }
}