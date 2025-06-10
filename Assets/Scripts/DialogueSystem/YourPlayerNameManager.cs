using UnityEngine;

public static class YourPlayerNameManager
{
    private static string _currentName;

    public static string CurrentName
    {
        get => _currentName;
        set => _currentName = value;
    }

    public static bool HasName()
    {
        return !string.IsNullOrEmpty(_currentName);
    }
    
}
