using System.IO;
using UnityEngine;

public static class APIKeyManager
{
    private static string apiKey;
    private static bool isInitialized = false;

    public static string GetAPIKey()
    {
        if (!isInitialized)
        {
            Initialize();
        }
        return apiKey;
    }

    private static void Initialize()
    {
        // Der relative Pfad zur Textdatei im Unterordner
        string relativePath = "Config/APIKey.txt";
        string path = Path.Combine(Application.dataPath, relativePath);

        if (File.Exists(path))
        {
            apiKey = File.ReadAllText(path);
            isInitialized = true;
        }
        else
        {
            Debug.LogError("APIKey.txt file not found at: " + path);
        }
    }
}
