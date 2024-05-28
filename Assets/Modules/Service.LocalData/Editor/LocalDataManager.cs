using System.IO;
using System.Linq;
using RpDev.Extensions;
using UnityEditor;
using UnityEngine;

namespace RpDev.Editor.LocalData
{
    public class LocalDataManger
    {
        #if UNITY_EDITOR
        [MenuItem("Tools/Delete all local data")]
        private static void DeleteAllLocalData()
        {
            var info = new DirectoryInfo(Application.persistentDataPath);
            info.GetFiles()
                .Where(file => file.Extension is ".sav" or ".bak" or ".tmp")
                .ForEach(file =>
                {
                    File.Delete(file.FullName);
                    Debug.Log($"--Data {file.Name} was deleted--");
                });

            PlayerPrefs.DeleteAll();
        }
        #endif
    }
}