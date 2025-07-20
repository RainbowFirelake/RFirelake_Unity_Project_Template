using RFirelake.Infrastructure.Logs;
using UnityEditor;
using UnityEngine;

namespace RFirelake.Editor
{
    public class ConfigurationsCreator
    {
        private const string ConfigurationPath = "Assets/_Template/Configurations/";

        [MenuItem("RFirelakeTemplate/Create Configuration Objects")]
        private static void CreateConfigurationObjects()
        {
            Debug.Log("Creating configuration objects...");

            CreateConfigurationSO<LoggerConfiguration>();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("Configuration objects creation successful!");
        }

        private static void CreateConfigurationSO<T>() where T : ScriptableObject
        {
            var typeName = typeof(T).Name;
            var assetGuids = AssetDatabase.FindAssets($"t: {typeName}");

            if (assetGuids.Length == 0)
            {
                Debug.Log($"Creating {typeName} object...");
                var instance = ScriptableObject.CreateInstance<T>();
                AssetDatabase.CreateAsset(instance, ConfigurationPath + typeName + ".asset");
            }
            else
            {
                Debug.Log($"{typeName} already exists in project!");
            }
        }
    }
}
