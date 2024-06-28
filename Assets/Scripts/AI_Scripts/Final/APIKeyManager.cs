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
        // Der relative Pfad zur Textdatei im StreamingAssets Unterordner
        string relativePath = "APIKey.txt";
        string path = Path.Combine(Application.streamingAssetsPath, relativePath);

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
